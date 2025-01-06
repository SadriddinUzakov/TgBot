using Infrastructure;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Handlers
{
    public class StartCommandHandler : ICommandHandler
    {
        private readonly TelegramBotClientWrapper _botClient;

        public StartCommandHandler(TelegramBotClientWrapper botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleAsync(string chatId)
        {
            var buttons = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("🇺🇿 O‘zbek", "lang_uz"),
                    InlineKeyboardButton.WithCallbackData("🇷🇺 Русский", "lang_ru"),
                    InlineKeyboardButton.WithCallbackData("🇬🇧 English", "lang_en")
                }
            });
            await _botClient.SendMessageAsync(chatId, "Welcome! Choose an option below:", buttons);
        }
    }
}
