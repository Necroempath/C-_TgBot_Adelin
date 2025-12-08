using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
// var bot = new TelegramBotClient("7817800414:AAHucHBLV0f0UNR3zgze9fjgmAFVdexFRDA");
//
// using var cts = new CancellationTokenSource();
//
//
// await bot.SetMyCommands(new[]
// {
//     new BotCommand { Command = "roll", Description = "/roll X - Roll X d10 dice\n/roll XdY - Roll X dY dice" },
//     new BotCommand { Command = "help", Description = "Show help message" }
// });

var json = File.ReadAllText("charset.json");

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var charsheet = JsonSerializer.Deserialize<Root>(json, options);

BotService botService = new("7817800414:AAHucHBLV0f0UNR3zgze9fjgmAFVdexFRDA", charsheet);
botService.Start();

Console.WriteLine("Bot is running...");

Console.ReadLine();


//bloodpool - 9/12
//cash - 2700$
// will - 9/10