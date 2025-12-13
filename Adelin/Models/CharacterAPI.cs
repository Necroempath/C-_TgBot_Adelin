using System.Text.Json;

namespace Adelin.Models;

public class CharacterAPI
{
    private Charsheet _character;

    public CharacterAPI(Charsheet character)
    {
        _character = character;
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

        string newJson = JsonSerializer.Serialize(_character, options);
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
                Name:\t{name}
                Age:\t{age}
                Clan:\t{clan}
                Generation:\t{gen}-th 
                Sire:\t{sire}\n
                Nature:\t{nature}
                """;
    }
}