using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Adelin.Models;
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
    public Dictionary<string, Command> Commands => _commands;
    
    public CommandHandler(CharacterAPI charsheet, Message msg)
    {
        _charsheet = charsheet;
        _msg = msg;
        _commands = new()
        {
            ["roll"] = new(Roll, "/roll [X] — Roll X d10 dice"),
            ["health"] = new(Health, "/health — show character health, cap"),
            ["damage"] = new(Damage, "/damage [damageType] ?[amount] — receive [damageType] [amount] times"),
            ["hello"] = new(Hello, ""),
            ["heal"] = new(Heal, ""),
            ["help"] = new(Help, "/help — show this message, cap")

        };
    }
    private string Roll(string[]? args)
    {
        var arg = args[0];
        if (arg is null)
        {
            return "/roll X or /roll XdY";
        }

        int diceCount = 0;
        int diceType = 10;

        var match = Regex.Match(arg, @"^(\d+)d(\d+)$");

        if (match.Success)
        {
            diceCount = int.Parse(match.Groups[1].Value);
            diceType = int.Parse(match.Groups[2].Value);
        }
        else if (int.TryParse(arg, out int simpleCount))
        {
            diceCount = simpleCount;
        }

        var rng = new Random();
        var rolls = Enumerable.Range(0, diceCount)
            .Select(_ => rng.Next(1, diceType + 1))
            .ToList();

        var response = new StringBuilder();
        rolls.ForEach(r => response.Append($"{r} + "));
        var result = rolls.Sum();
        response[^2] = '=';
        response.Append($" {result}");
        
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

        return response.ToString();
    }

    private string Damage(string[]? args)
    {
        if (args is null || args.Length == 0)
        {
            return "Specify damage to apply, e.g /damage bashing 2";
        }
        
        int count = 1;
        string damageType = args.Last();

        if (int.TryParse(args.Last(), out var temp))
        {
            if (args.Length < 2)
            {
                return "Specify damage to apply, e.g /damage bashing 2";
            }
            
            damageType = args[^2];
            if (temp > 0)
            {
                count = temp;
            }
        }

        int damageValue;
            
        if(Enum.TryParse<DamageType>(damageType, ignoreCase: true, out var type))
        {
            damageValue = (int)type;    
        }
        else
        {
            return "Specify damage to apply, e.g /damage bashing 2";
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
        var count = 1;
        var damageType = 0;
        
        if (args is null)
        {
            
        }

        return "";
    }

    private bool validateArguments(string[]? args)
    {
        return true;
    }
    
    private string Hello(string[]? args = null)
    {
        var user = _msg.From.FirstName;
        return $"Hello, {user}!";
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