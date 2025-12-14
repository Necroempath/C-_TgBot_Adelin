using System.Text.Json;
using Adelin.Models;

var json = File.ReadAllText("charset.json");

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var root = JsonSerializer.Deserialize<Root>(json, options)!;

BotService botService = new("7817800414:AAHucHBLV0f0UNR3zgze9fjgmAFVdexFRDA", new CharacterAPI(root));
botService.Start();

Console.WriteLine("Bot is running...");

Console.ReadLine();


//bloodpool - 10/12
//cash - 3200$
// will - 10/10