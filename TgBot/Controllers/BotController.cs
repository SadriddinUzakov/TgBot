using Application.Handlers;
using Infrastructure;

namespace TgBot.Controllers
{
    public class BotController
    {
        private readonly TelegramBotClientWrapper _botClient;
        private readonly Dictionary<string, ICommandHandler> _commandHandlers;

        public BotController(string token)
        {
            _botClient = new TelegramBotClientWrapper(token);

            // Buyruqlarni ro'yxatdan o'tkazish
            _commandHandlers = new Dictionary<string, ICommandHandler>
                {
                    { "/start", new StartCommandHandler(_botClient) },
                    { "/help", new HelpCommandHandler(_botClient) },
                    { "/info", new InfoCommandHandler(_botClient) },
                    { "/language", new LanguageCommandHandler(_botClient) }
                };
        }

        public async Task RunAsync()
        {
            // Menyuni o'rnatish
            await _botClient.SetCommandsAsync();

            await _botClient.ListenUpdatesAsync(async update =>
            {
                if (update.Message is not null)
                {
                    var chatId = update.Message.Chat.Id.ToString();
                    var message = update.Message.Text;

                    if (message != null && _commandHandlers.ContainsKey(message))
                    {
                        await _commandHandlers[message].HandleAsync(chatId, message);
                    }
                    else
                    {
                        await _botClient.SendMessageAsync(chatId, "Unknown command or message is empty. Use /help for a list of commands.");
                    }
                }

                else if (update.CallbackQuery is not null)
                {
                    var chatId = update.CallbackQuery.Message.Chat.Id.ToString();
                    var callbackData = update.CallbackQuery.Data;

                    if (_commandHandlers.ContainsKey(callbackData))
                    {
                        await _commandHandlers[callbackData].HandleAsync(chatId, callbackData);
                    }
                }
            });
        }
    }
}
