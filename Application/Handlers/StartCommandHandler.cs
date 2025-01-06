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

        public async Task HandleAsync(string chatId, string? callbackData = null)
        {
            if (callbackData == null)
            {
                // Til tanlash tugmalarini yuborish
                var message = "Tilni tanlang:\nВыберите язык:\nChoose a language:";
                var buttons = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("🇺🇿 O‘zbek", "lang_uz"),
                        InlineKeyboardButton.WithCallbackData("🇷🇺 Русский", "lang_ru"),
                        InlineKeyboardButton.WithCallbackData("🇬🇧 English", "lang_en")
                    }
                });

                await _botClient.SendMessageAsync(chatId, message, buttons);
            }
            else
            {
                // Til tanlanganidan keyin foydalanuvchiga telefon raqami yuborishni so‘rashi uchun button
                await ShowContactButton(chatId, callbackData);
            }
        }

        private async Task ShowContactButton(string chatId, string callbackData)
        {
            // Til tanlanganidan keyin telefon raqamini so'rash uchun xabar
            var phoneRequestMessage = callbackData switch
            {
                "lang_uz" => "📞 Iltimos, telefon raqamingizni yuboring.",
                "lang_ru" => "📞 Пожалуйста, отправьте свой номер телефона.",
                "lang_en" => "📞 Please send your phone number.",
                _ => "📞 Please send your phone number."
            };

            // Kontaktni so'rash uchun tugma
            var contactButton = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("📱 Telefon raqamni yuborish") { RequestContact = true }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };

            // Telefon raqamni yuborish tugmasini foydalanuvchiga yuborish
            await _botClient.SendMessageAsync(chatId, phoneRequestMessage, contactButton);
        }
    }
}
