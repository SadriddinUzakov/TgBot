using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Handlers
{
    public class LanguageCommandHandler : ICommandHandler
    {
        private readonly TelegramBotClientWrapper _botClient;

        public LanguageCommandHandler(TelegramBotClientWrapper botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleAsync(string chatId, string? userMessage = null)
        {
            var buttons = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "🇺🇿 O‘zbek", "🇷🇺 Русский", "🇬🇧 English" }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true 
            };
            await _botClient.SendMessageAsync(chatId, "Choose your preferred language to change:", replyMarkup: buttons);
        }
    }
}
