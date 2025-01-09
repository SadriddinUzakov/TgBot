using Infrastructure;
using Npgsql;
using Telegram.Bot;
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
            await SavePhoneNumberToDatabase(phoneNumber);

            await _botClient.SendMessageAsync(message.Chat.Id.ToString(), "Raqamingiz muvaffaqiyatli saqlandi.");
        }

        private async Task SavePhoneNumberToDatabase(string phoneNumber)
        {
            var connectionString = "Host=localhost;Username=postgres;Password=ssss1111;Database=tg_bot";

            await using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (var cmd = new NpgsqlCommand("INSERT INTO users (phone_number) VALUES (@p)", conn))
                {
                    cmd.Parameters.AddWithValue("p", phoneNumber);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}