using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Adelin.Models;

namespace Adelin;
enum DamageType
{
    Bashing = 1,
    Lethal = 2,
    Aggregated = 3
}

public class CommandHandler
{
    private readonly Root _charsheet;
    private Dictionary<string, Command> _commands;
    public Dictionary<string, Command> Commands => _commands;
    
    public CommandHandler(Root charsheet)
    {
        _charsheet = charsheet;
        _commands = new()
        {
            ["roll"] = new(Roll, "/roll [X] — Roll X d10 dice"),
            ["health"] = new(Health, "/health — show character health, cap"),
            ["damage"] = new(Damage, "/damage [damageType] ?[amount] — receive [damageType] [amount] times"),
            ["help"] = new(Help, "/help — show this message, cap"),
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

        var hp = _charsheet.Charsheet.Health;
        var response = new StringBuilder();
        var props = hp.GetType().GetProperties();
        foreach (var prop in props)
        {
            var value = (int)prop.GetValue(hp)!;

            if (value == 0)
            {
                continue;
            }

            var name = prop.Name;

            response.AppendLine($"{name}: {damages[value]}");
        }

        return response.ToString();
    }

    private string Damage(string[]? args)
    {
        if (args is null)
        {
            return "Specify damage to apply, e.g /damage bashing 2";
        }
        
        int count = 1;
        string damageType = args.Last();

        if (int.TryParse(args.Last(), out var temp))
        {
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
        
        var hp = _charsheet.Charsheet.Health;
        var props = hp.GetType().GetProperties();
        var states = props.Select(v => (int)v.GetValue(hp)!).ToList();
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < states.Count; j++)
            {
                if (damageValue > states[j] || states[j] == 0)
                {
                    states[j] = damageValue;
                    break;
                }
            }
        }
            
        for(int i = 0; i < props.Length; i++)
        {
            props[i].SetValue(hp, states[i]);
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string newJson = JsonSerializer.Serialize(_charsheet, options);
        File.WriteAllText("charset.json", newJson);
        
        return Health(null);
    }

    public string Help(string[]? args = null)
    {
        StringBuilder sb = new();
        
        foreach (var command in _commands.Values)
        {
            sb.AppendLine(command.Description); 
        }
        
        return sb.ToString();
    }
}