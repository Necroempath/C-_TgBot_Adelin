using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Adelin.Abstractions;
using Adelin.Models;
using Adelin.Realizations;
using Adelin.Storage;
using Telegram.Bot.Types;

namespace Adelin;
enum DamageType
{
    Bashing = 1,
    Lethal = 2,
    Aggregated = 3
}

public class CommandHandler
{
    private readonly CharacterAPI _charsheet;
    private readonly Message _msg;
    private Dictionary<string, Command> _commands;
    IEnumerable<IRollStat> attributeStats = new AttributeRegistry().All;
    IEnumerable<IRollStat> abilityStats = new AbilityRegistry().All;
    IEnumerable<IRollStat> disciplineStats = new DisciplineRegistry().All;
    IEnumerable<IRollStat> virtueStats     = new VirtueRegistry().All;
    IEnumerable<IRollStat> stateStats      = new StateRegistry().All;
    RollStatRegistry rollRegistry;
    
    public Dictionary<string, Command> Commands => _commands;
    
    public CommandHandler(CharacterAPI charsheet, Message msg)
    {
        IEnumerable<IRollStat> allRollStats =
            attributeStats
                .Concat(abilityStats)
                .Concat(disciplineStats)
                .Concat(virtueStats)
                .Concat(stateStats);
        
        rollRegistry = new RollStatRegistry(allRollStats);
        _charsheet = charsheet;
        _msg = msg;
        _commands = new()
        {
            ["roll"] = new(Roll, "/roll [X] — Roll X d10 dice"),
            ["health"] = new(Health, "/health — show character health, cap"),
            ["damage"] = new(Damage, "/damage [damage type] ?[amount] — receive [damageType] [amount] times"),
            ["heal"] = new(Heal, "/heal [wound type] ?[amount] — heal [woundType] [amount] times"),
            ["state"] = new(State, "/state show character state"),
            ["virtues"] = new(Virtues, "/virtues show character virtues"),
            ["get"] = new(GetStat, "/get [state name] get the current value of specified person state"),
            ["set"] = new(SetStat, "/set [state name] set new value to specified person state"),
            ["add"] = new(AddToStat, "/add [state name] add new value to specified person state"),
            ["attributes"] = new(GetAttributes, "/attributes show person attributes"),
            ["abilities"] = new(GetAbilities, "/abilities show person abilities"),
            ["disciplines"] = new(GetDisciplines, "/disciplines show person disciplines"),
            ["sebastian"] = new(Sebastian, "/sebastian show character general info"),
            ["help"] = new(Help, "/help — show this message, cap")

        };
    }
    private string Roll(string[]? args)
    {
        if (args is null || args.Length == 0)
        {
            return Help();
        }
        
        var response = new StringBuilder();
        if (!int.TryParse(args[0], out int diceCount))
        {
            var sum = 0;
            
            foreach (var token in args)
            {
                var stat = rollRegistry.Find(token);
                
                if (stat is null)
                {
                    return Help();
                }
                int current = stat.GetValue(_charsheet.GetCharacter());
                sum += current;
                response.AppendLine($"{stat.Aliases[0]} ({current})");
            }
            
            diceCount = sum;
        }

        var rng = new Random();
        var rolls = Enumerable.Range(0, diceCount)
            .Select(_ => rng.Next(1, 11))
            .ToList();

        
        rolls.ForEach(r => response.Append($"{r} + "));
        var result = rolls.Sum();
        response[^2] = '=';
        response.AppendLine($" {result}");
        
        return response.ToString();
     }

    private string Health(string[]? args = null)
    {
        var damages = new[] { ' ', '/', 'X', '*' };
        
        var response = new StringBuilder();

        var hpStates = _charsheet.GetHealth();
        
        foreach (var hp in hpStates)
        {
            if (hp.Item2 == 0)
            {
                continue;
            }

            response.AppendLine($"{hp.Item1}: {damages[hp.Item2]}");
        }

        return response.ToString().Length == 0 ? "Full health" : response.ToString();
    }

    private string Damage(string[]? args)
    {
        bool isValid = ValidateDamageAndHealing(args, out int damageValue, out int count);

        if (!isValid)
        {
            return "Specify damage to apply e.g /damage bashing 2";
        }
        
        var states = _charsheet.GetHealth().Select(_ => _.Item2).ToArray();
        
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < states.Length; j++)
            {
                if (damageValue > states[j] || states[j] == 0)
                {
                    states[j] = damageValue;
                    break;
                }
            }
        }
            
        _charsheet.SetHealth(states);
        
        return Health();
    }

    private string Heal(string[]? args)
    {
        if (args is not null && args[0] == "all")
        {
            _charsheet.SetHealth(new int[7]);
            return Health();
        }
        bool isValid = ValidateDamageAndHealing(args, out int healValue, out int count);

        if (!isValid)
        {
            return "Specify wound to heal e.g /heal lethal";
        }
        
        var states = _charsheet.GetHealth().Select(_ => _.Item2).ToArray();

        for (int i = 0; i < count; i++)
        {
            bool healed = false;
            
            for (int j = 0; j < states.Length; j++)
            {
                if (states[j] == healValue)
                {
                    states[j] = 0;
                    healed = true;
                    break;
                }
            }
            
            if(!healed)
            {
                break;
            }
        }
        
        _charsheet.SetHealth(states);
         
        return Health();
    }

    private bool ValidateDamageAndHealing(string[]? args, out int type, out int count)
    {
        type = 0;
        count = 0;
        
        if (args is null || args.Length == 0)
        {
            return false;
        }
        
        count = 1;
        string damageType = args.Last();

        if (int.TryParse(args.Last(), out var temp))
        {
            if (args.Length < 2)
            {
                return false;
            }
            
            damageType = args[^2];
            if (temp > 0)
            {
                count = temp;
            }
        }
        
            
        if(Enum.TryParse<DamageType>(damageType, ignoreCase: true, out var damageValue))
        {
            type = (int)damageValue;    
        }
        else
        {
            return false;
        }

        return true;
    }

    private string State(string[]? args = null)
    {
        return _charsheet.GetState();
    }

    private string Virtues(string[]? args = null)
    {
        return _charsheet.GetVirtues();
    }
    private string Sebastian(string[]? args = null)
    {
        return _charsheet.GetProfile();
    }

    private string GetStat(string[]? args)
    {
        if (!(args is null || args.Length != 1))
        {
            var result = _charsheet.TryGetValue(args[0]);

            return result;
        }

        return Help();
    }
    private string SetStat(string[]? args)
    {
        bool valid = !(args is null || args.Length != 2);
        valid = int.TryParse(args[1], out var value);
        valid = valid && value > 0;

        if (valid)
        {
            var result = _charsheet.TrySetValue(args[0], value);

            return result;
        }
        
        return Help();
    }

    private string AddToStat(string[]? args)
    {
        bool valid = !(args is null || args.Length != 2);
        valid = int.TryParse(args[1], out var value);

        if (valid)
        {
            var result = _charsheet.TryAddValue(args[0], value);

            return result;
        }
        
        return Help();
    }

    private string GetAttributes(string[]? args = null)
    {
        return _charsheet.GetAttributes();
    }

    private string GetDisciplines(string[]? args = null)
    {
        return _charsheet.GetDisciplines();
    }

    private string GetAbilities(string[]? args = null)
    {
        return _charsheet.GetAbilities();
    }
    public string Help(string[]? args = null)
    {
        StringBuilder sb = new();
        
        foreach (var command in _commands.Values)
        {
            if(command.Description!.Length > 0) sb.AppendLine(command.Description); 
        }
        
        return sb.ToString();
    }
}