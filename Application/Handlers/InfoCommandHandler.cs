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
                          "Ру: Отслеживайте свой бизнес на каждом этапе и сделайте его успешном вместе с DT Ecosystem\n" +
                          "- O‘z: Har bir bosqichda biznesingizni kuzatib boring va uni DT Ecosystem bilan birgalikda muvaffaqiyatli qiling\n" +
                          "- En: Track your business at every stage and make it successful together with DT Ecosystem\n";

            await _botClient.SendMessageAsync(chatId, botInfo, null);
        }
    }
}
