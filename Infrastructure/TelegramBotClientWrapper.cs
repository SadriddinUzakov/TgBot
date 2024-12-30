using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Infrastructure
{
    public class TelegramBotClientWrapper
    {
        private readonly TelegramBotClient _client;

        public TelegramBotClientWrapper(string token)
        {
            _client = new TelegramBotClient(token);
        }

        public async Task ListenUpdatesAsync(Func<Update, Task> onUpdate)
        {
            var offset = 0;
            while (true)
            {
                var updates = await _client.GetUpdatesAsync(offset);
                foreach (var update in updates)
                {
                    await onUpdate(update);
                    offset = update.Id + 1;
                }
            }
        }

        public async Task SendMessageAsync(string chatId, string message, InlineKeyboardMarkup? buttons = null)
        {
            await _client.SendTextMessageAsync(chatId, message, replyMarkup: buttons);
        }

        public async Task SetCommandsAsync()
        {
            var commands = new[]
            {
                new BotCommand { Command = "/start", Description = "Start bot" },
                new BotCommand { Command = "/help", Description = "Help information" },
                new BotCommand { Command = "/info", Description = "Information about the bot" },
                new BotCommand { Command = "/upload_word", Description = "Upload Word file" },
                new BotCommand { Command = "/upload_excel", Description = "Upload Excel file" },
                new BotCommand { Command = "/upload_powerpoint", Description = "Upload PowerPoint file" }
            };

            await _client.SetMyCommandsAsync(commands);
        }
    }
}
