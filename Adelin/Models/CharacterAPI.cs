using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Adelin.Models;

public class CharacterAPI
{
    private Root _root;
    private Charsheet _character;
    private Dictionary<string, StatDescriptor> _stats;

    public CharacterAPI(Root root)
    {
        _root = root;
        _character = root.Charsheet;
        _stats = new()
        {
            ["bloodpool"] = new ()
            {
                Get = () => _character.State.Bloodpool,
                Set = value => _character.State.Bloodpool = value,
                Add = value => _character.State.Bloodpool += value,
                Min = 0,
                Max = 10
            }
        };
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
        
        return $"""
                Willpower rating {willpowerRating}
                Willpower pool: {willpowerPool}
                Bloodpool: {bloodpool}
                Humanity: {humanity}
                Experience: {experience}
                """;   
    }
    public string GetVirtues()
    {
        var props = _character.Virtues.GetType().GetProperties();
        
        StringBuilder sb = new();
        
        props.ToList().ForEach(p => sb.AppendLine($"{p.Name}: {p.GetValue(_character.Virtues)}"));
        return sb.ToString();
    }

    public string? SetValueToProp(string propName, int value)
    {
        var found = _properties.TryGetValue(propName.ToLower(), out var prop);

        if (!found)
        {
            return null;
        }
     
        prop!.SetValue(prop, value);
        SaveToJson();
        
        return $"{prop.Name}: {value}";
    }
}