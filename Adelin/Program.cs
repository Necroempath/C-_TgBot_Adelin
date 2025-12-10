using System.Text.Json;
using Adelin.Models;

var json = File.ReadAllText("charset.json");

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var charsheet = JsonSerializer.Deserialize<Charsheet>(json, options)!;

BotService botService = new("7817800414:AAHucHBLV0f0UNR3zgze9fjgmAFVdexFRDA", new CharacterAPI(charsheet));
botService.Start();

Console.WriteLine("Bot is running...");

Console.ReadLine();


//bloodpool - 9/12
//cash - 2700$
// will - 9/10