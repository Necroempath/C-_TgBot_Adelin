using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Adelin;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class BotService
{
    private readonly ITelegramBotClient _bot;
    private readonly Root _charsheet;

    public BotService(string token, Root sheet)
    {
        _bot = new TelegramBotClient(token);
        _charsheet = sheet;
    }

    public void Start()
    {
        _bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() }
        );
    }

    private async Task HandleUpdateAsync(
        ITelegramBotClient bot,
        Update update,
        CancellationToken token)
    {
        if (update.Message is not { } msg || msg.Text is not { } input || !input.StartsWith('/'))
            return;
        
        string response;
        
        var tokens = input.Trim().TrimStart('/').Split(' ');
        
        var commandKey = tokens[0];
        var args = tokens.Skip(1).ToArray();
        
        CommandHandler commandHandler = new(_charsheet);
        
        var isCommandExist = commandHandler.Commands.TryGetValue(commandKey.ToLower(), out var command);

        if (!isCommandExist)
        {
            response = commandHandler.Help();
        }
        else
        {
            response = command?.Handler.Invoke(args)!;
        }
        
        await bot.SendMessage(msg.Chat.Id, response, cancellationToken: token);
    }
    
    private Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken token)
    {
        Console.WriteLine(ex);
        return Task.CompletedTask;
    }
}