using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Adelin.Models;

public class CharacterAPI
{
    private Root _root;
    private Charsheet _character;
    private Dictionary<string, StatDescriptor> _stats;
    private Dictionary<string, string> _attributes = new(9);

    public CharacterAPI(Root root)
    {
        _root = root;
        _character = root.Charsheet;
        _stats = new()
        {
            ["bp"] = new ()
            {
                Get = () => (nameof(_character.State.Bloodpool), _character.State.Bloodpool),
                Set = value => _character.State.Bloodpool = value,
                Add = value => _character.State.Bloodpool += value,
                Min = 0,
                Max = 12
            },
            ["wp"] = new ()
            {
                Get = () => (nameof(_character.State.WillpowerRating), _character.State.WillpowerRating),
                Set = value => _character.State.WillpowerRating = value,
                Add = value => _character.State.WillpowerRating += value,
                Min = 0,
                Max = 10
            },
            ["money"] = new ()
            {
                Get = () => (nameof(_character.Money), _character.Money),
                Set = value => _character.Money = value,
                Add = value => _character.Money += value,
                Min = 0,
                Max = int.MaxValue
            },
            ["exp"] = new ()
            {
                Get = () => (nameof(_character.State.Experience), _character.State.Experience),
                Set = value => _character.State.Experience = value,
                Add = value => _character.State.Experience += value,
                Min = 0,
                Max = int.MaxValue
            }
        };
        var att = _character.Attributes;
    }
    
    public IEnumerable<(string, int)> GetHealth()
    {
        var hp = _character.Health;
        var props = hp.GetType().GetProperties();
        return props.Select(v => (v.Name, (int)v.GetValue(hp)!));
    }

    public void SetHealth(int[] states)
    {
        var hp = _character.Health;
        var props = hp.GetType().GetProperties();

        for(int i = 0; i < props.Length; i++)
        {
            props[i].SetValue(hp, states[i]);
        }

        SaveToJson();
    }

    private void SaveToJson()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string newJson = JsonSerializer.Serialize(_root, options);
        File.WriteAllText("charset.json", newJson);
    }

    public string GetProfile()
    {
        var name = _character.Profile.Name;
        var age = _character.Profile.Age;
        var clan = _character.Profile.Clan;
        var gen = _character.Profile.Generation;
        var sire = _character.Profile.Sire;
        var nature = _character.Profile.Nature;
        
        return $"""
                Name {name}
                Age: {age}
                Clan: {clan}
                Generation: {gen}-th
                Sire: {sire}
   
                Nature: {nature}
                """;   
    }
    
    public string GetState()
    {
        var willpowerRating = _character.State.WillpowerRating;
        var willpowerPool = _character.State.WillpowerPool;
        var bloodpool = _character.State.Bloodpool;
        var humanity = _character.State.Humanity;
        var experience = _character.State.Experience;
        var money = _character.Money;
        
        return $"""
                Willpower rating {willpowerRating}
                Willpower pool: {willpowerPool}
                Bloodpool: {bloodpool}
                Humanity: {humanity}
                Experience: {experience}
                Money: {money}
                """;   
    }
    public string GetVirtues()
    {
        var props = _character.Virtues.GetType().GetProperties();
        
        StringBuilder sb = new();
        
        props.ToList().ForEach(p => sb.AppendLine($"{p.Name}: {p.GetValue(_character.Virtues)}"));
        return sb.ToString();
    }

    public string TryGetValue(string key)
    {
        if (!_stats.TryGetValue(key, out var stat))
        {
            return "Invalid stat name";
        }
        
        var desc = stat.Get();

        return $"{desc.Item1}: {desc.Item2}";
    }
    public string TrySetValue(string key, int value)
    {
        if (!_stats.TryGetValue(key, out var stat))
        {
            return "Invalid stat name";
        }

        if (value < stat.Min || value > stat.Max)
        {
            return $"Value {value} is out of range. Min: {stat.Min}, Max: {stat.Max}";
        }
        
        stat.Set(value);
        SaveToJson();
        var desc = stat.Get();

        return $"{desc.Item1}: {value}";
    }

    public string TryAddValue(string key, int value)
    {
        if (!_stats.TryGetValue(key, out var stat))
        {
            return "Invalid stat name";
        }
        var desc = stat.Get();
        var newValue = desc.Item2 + value;
        
        if (newValue  < stat.Min || newValue > stat.Max)
        {
            return $"New value {newValue} is out of range. Min: {stat.Min}, Max: {stat.Max}";
        }
        
        stat.Set(newValue);
        SaveToJson();
        
        
        return $"{desc.Item1}: {newValue}";
    }

    public string GetAttributes()
    {
        StringBuilder sb = new();

        // var props = _character.Attributes.GetType().GetProperties();
        // props.ToList().ForEach(p => sb.AppendLine($"{_attributes[p.Name]}: {p.GetValue(_character.Attributes)}"));
        var registry = new AttributeRegistry();

        foreach (var attr in registry.All)
        {
            sb.AppendLine($"{attr.Aliases[0]}: {attr.GetValue(_character)}");
        }
        return sb.ToString();
    }
}