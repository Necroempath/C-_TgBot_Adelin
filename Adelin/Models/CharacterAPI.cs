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
}