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
                    InlineKeyboardButton.WithCallbackData("Help", "/help"),
                    InlineKeyboardButton.WithCallbackData("More Info", "/info")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Upload Word", "/upload_word"),
                    InlineKeyboardButton.WithCallbackData("Upload Excel", "/upload_excel"),
                    InlineKeyboardButton.WithCallbackData("Upload PowerPoint", "/upload_powerpoint")
                }
            });

            await _botClient.SendMessageAsync(chatId, "Welcome! Choose an option below:", buttons);
        }
    }
}
