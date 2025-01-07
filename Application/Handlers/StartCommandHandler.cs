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
            // Xush kelibsiz matni
            var botInfo = "🤖 Salom!\n\n" +
                          "Bizning Botga xush kelibsiz!\n" +
                          "Xizmat ko'rsatish tilini tanlang:\n\n" +
                          "🤖 Здравствуйте!\n\n" +
                          "Добро пожаловать в нашего бота!\n" +
                          "Выберите язык обслуживания:\n\n" +
                          "🤖 Hello!\n\n" +
                          "Welcome to our bot!\n" +
                          "Choose the service language:\n";

            await _botClient.SendMessageAsync(chatId, botInfo);

            // ReplyKeyboardMarkup tugmalari
            var buttons = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "🇺🇿 O‘zbek", "🇷🇺 Русский", "🇬🇧 English" }
            })
            {
                ResizeKeyboard = true, // Tugmalar ekranga mos ravishda kichrayadi
                OneTimeKeyboard = true // Tugmalar bir marta ko'rinadi
            };

            await _botClient.SendMessageAsync(chatId, "Choose your preferred language below:", replyMarkup: buttons);
        }
    }
}
