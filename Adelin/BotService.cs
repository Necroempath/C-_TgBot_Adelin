using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

enum DamageType
{
    Bashing = 1,
    Lethal = 2,
    Aggregated = 3
}

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
        if (update.Message is not { } msg || msg.Text is not { } text)
            return;
        text = text.Trim();

        if (text == "/health")
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

            await bot.SendMessage(msg.Chat.Id, $"{response}", cancellationToken: token);
            return;
        }

        if (text.StartsWith("/damage"))
        {
            var tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int count = 1;
            string damageType = tokens.Last();

            if (int.TryParse(tokens.Last(), out var temp))
            {
                damageType = tokens[^2];
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
                return;
            }
            
            // / X  [X][X]   X X X 
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
                WriteIndented = true, // красиво форматируем
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string newJson = JsonSerializer.Serialize(_charsheet, options);
            File.WriteAllText("charset.json", newJson);
            // await bot.SendMessage(msg.Chat.Id, $"Not ready, loser. First, construct me!", cancellationToken: token);
            return;
        }

        if (text.StartsWith("/roll"))
        {
            var parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                await bot.SendMessage(msg.Chat.Id, "Usage: /roll X or /roll XdY", cancellationToken: token);
                return;
            }

            int diceCount = 0;
            int diceType = 10;

            var input = parts[1];

            var match = Regex.Match(input, @"^(\d+)d(\d+)$");

            if (match.Success)
            {
                diceCount = int.Parse(match.Groups[1].Value);
                diceType = int.Parse(match.Groups[2].Value);
            }
            else if (int.TryParse(input, out int simpleCount))
            {
                diceCount = simpleCount;
            }

            var rng = new Random();
            var rolls = Enumerable.Range(0, diceCount)
                .Select(_ => rng.Next(1, diceType + 1))
                .ToList();

            var builder = new StringBuilder();
            rolls.ForEach(r => builder.Append($"{r} + "));
            var result = rolls.Sum();
            builder[^2] = '=';
            builder.Append($" {result}");

            await bot.SendMessage(msg.Chat.Id, $"🎲 Result: {builder}", cancellationToken: token);
        }

        if (text.StartsWith("/help"))
        {
            string helpMsg = @"
Available commands:
/roll X        - Roll X d10 dice
/roll XdY      - Roll X dY dice
/help          - Show this message
";
            await bot.SendMessage(msg.Chat.Id, helpMsg, cancellationToken: token);
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken token)
    {
        Console.WriteLine(ex);
        return Task.CompletedTask;
    }
}