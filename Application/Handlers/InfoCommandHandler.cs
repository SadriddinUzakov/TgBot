using Infrastructure;

namespace Application.Handlers
{
    public class InfoCommandHandler : ICommandHandler
    {
        private readonly TelegramBotClientWrapper _botClient;

        public InfoCommandHandler(TelegramBotClientWrapper botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleAsync(string chatId, string userMessage)
        {
            var botInfo = "🤖 Salom!\n\n" +
                          "Bizning Botga xush kelibsiz!\n" +
                          "Xizmat ko'rsatish tilini tanlang:\n\n" +
                          "🤖 Здравствуйте!\n\n" +
                          "Добро пожаловать в нашего бота!\n" +
                          "Выберите язык обслуживания:\n\n" +
                          "🤖 Hello!\n\n" +
                          "Welcome to our bot!\n" +
                          "Choose the service language:\n";

            await _botClient.SendMessageAsync(chatId, botInfo, null);
        }
    }
}