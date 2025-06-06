using Miri_sDiceRoller;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


using var cts = new CancellationTokenSource();

Console.WriteLine("Введите токен вашего бота");
string? botToken = Console.ReadLine();
var roller = new Roller();
if (botToken == null)
{
    Environment.Exit(1);
}
var bot = new TelegramBotClient(botToken);
var me = await bot.GetMe();
bot.OnMessage += OnMessage;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel(); // stop the bot+

async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception); // just dump the exception to the console
}

// method that handle messages received by the bot:
async Task OnMessage(Message msg, UpdateType type)
{
    if (msg.Text is null) return;	// we only handle Text messages here
    if (msg.Text == "/start")
    {
        Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");
        // let's echo back received text in the chat
        await bot.SendMessage(msg.Chat, $"{msg.From?.Username}, Привет! :3");
    }
    else if (msg.Text.StartsWith("/roll") || msg.Text.StartsWith("/Roll"))
    {
        await bot.SendMessage(msg.Chat, msg.From.Username + '\n' + roller.NewRoll(msg.Text.ToLower()));
    }
}
