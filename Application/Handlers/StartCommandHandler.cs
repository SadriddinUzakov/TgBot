using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                }
            });

            await _botClient.SendMessageAsync(chatId, "Welcome! Choose an option below:", buttons);
        }
    }
}
