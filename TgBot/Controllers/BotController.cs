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
            await _botClient.SetCommandsAsync();

            await _botClient.ListenUpdatesAsync(async update =>
            {
                if (update.Message is not null)
                {
                    var chatId = update.Message.Chat.Id.ToString();
                    var message = update.Message.Text;

                    if (message != null && _commandHandlers.ContainsKey(message))
                    {
                        // Handle commands like /start, /help, etc.
                        await _commandHandlers[message].HandleAsync(chatId, message);
                    }
                    else if (update.Message.Contact != null)
                    {
                        // Save the contact number
                        var contactHandler = new ContactCommandHandler(_botClient);
                        await contactHandler.HandleContactAsync(update.Message);
                    }
                    else if (update.Message.Text != null)
                    {
                        // If the message is not a command, check if it's a language choice
                        var languageHandler = new LanguageCommandHandler(_botClient);
                        var selectedLanguage = update.Message.Text;

                        if (selectedLanguage == "🇺🇿 O‘zbek" || selectedLanguage == "🇷🇺 Русский" || selectedLanguage == "🇬🇧 English")
                        {
                            // Save the selected language and send a confirmation
                            await languageHandler.SetUserLanguageAsync(chatId, selectedLanguage);
                        }
                        else
                        {
                            // Default unknown command
                            await _botClient.SendMessageAsync(chatId, "Noma'lum buyruq. Iltimos, /language komandasini tanlang.");
                        }
                    }
                }
            });
        }
    }
}