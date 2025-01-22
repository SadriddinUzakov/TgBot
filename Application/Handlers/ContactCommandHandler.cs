using Infrastructure;
using Npgsql;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Handlers
{
    public class ContactCommandHandler : ICommandHandler
    {
        private readonly TelegramBotClientWrapper _botClient;

        public ContactCommandHandler(TelegramBotClientWrapper botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleAsync(string chatId, string userMessage)
        {
            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new[] { KeyboardButton.WithRequestContact("Raqamingizni yuboring") }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };

            await _botClient.SendMessageAsync(chatId, "Iltimos, telefon raqamingizni yuboring:", replyMarkup: keyboard);
        }

        public async Task HandleContactAsync(Message message)
        {
            var phoneNumber = message.Contact.PhoneNumber;

            if (await IsUserRegisteredOnSite(phoneNumber))
            {
                await _botClient.SendMessageAsync(message.Chat.Id.ToString(), "Raqamingiz botda muvaffaqiyatli tasdiqlandi va foydalanishingiz mumkin.");
            }
            else
            {
                string registrationLink = "https://id.dt.uz/register"; // Registreation from site
                await _botClient.SendMessageAsync(message.Chat.Id.ToString(),
                    $"Siz bizning saytdan ro'yxatdan o'tmagansiz. Iltimos, quyidagi havola orqali ro'yxatdan o'ting: {registrationLink}");
            }
        }

        private async Task<bool> IsUserRegisteredOnSite(string phoneNumber)
        {
            var connectionString = "Host=192.168.100.232;Port=5432;Username=postgres;Password=vs658hi2jsVjDf57;Database=dt_xabar";

            await using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE phone_number = @p", conn))
                {
                    cmd.Parameters.AddWithValue("p", phoneNumber);
                    var count = (long)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }
    }
}
