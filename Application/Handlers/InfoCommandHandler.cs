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

        public async Task HandleAsync(string chatId)
        {
            var botInfo = "🤖 *Bot haqida ma'lumot:*\n\n" +
                          "- Bot: Bu botda siz File tashlay olasiz.\n" +
                          "- Xizmatlar:File lar bilan ishlash.\n" +
                          "- Creator: Uzakov.S.\n" +
                          "- Versiya: 1.0.0\n\n" +
                          "Savollar uchun admin bilan bog'laning : @uzakovv_1.";

            await _botClient.SendMessageAsync(chatId, botInfo, null);
        }
    }
}
