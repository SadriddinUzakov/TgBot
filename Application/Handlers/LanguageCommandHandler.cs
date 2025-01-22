using Application.Handlers;
using Domain.Lang;
using Domain.Lang.uz;
using Infrastructure;
using Telegram.Bot.Types.ReplyMarkups;

public class LanguageCommandHandler : ICommandHandler
{
    private readonly TelegramBotClientWrapper _botClient;
    private static readonly Dictionary<string, string> UserLanguages = new();

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

        await _botClient.SendMessageAsync(chatId, Messages.SELECT_LANGUAGE, replyMarkup: buttons);
    }

    public async Task SetUserLanguageAsync(string chatId, string language)
    {
        UserLanguages[chatId] = language;

        string responseMessage = language switch
        {
            "🇺🇿 O‘zbek" => Messages.LANGUAGE_CHANGED,
            "🇷🇺 Русский" => Messages.LANGUAGE_CHANGED,
            "🇬🇧 English" => Messages.LANGUAGE_CHANGED,
            _ => Messages.LANGUAGE_CHANGED, // Default to English
        };

        await _botClient.SendMessageAsync(chatId, responseMessage);

        // Foydalanuvchidan telefon raqamini so'rash
        await AskForContact(chatId, language);
    }

    private async Task AskForContact(string chatId, string language)
    {
        string contactMessage = language switch
        {
            "🇺🇿 O‘zbek" => Messages.SEND_CONTACT_MESSAGE,
            "🇷🇺 Русский" => Messages.SEND_CONTACT_MESSAGE,
            "🇬🇧 English" => Messages.SEND_CONTACT_MESSAGE,
            _ => Messages.SEND_CONTACT_MESSAGE, // Default to English
        };

        var contactButton = new KeyboardButton(Messages.SEND_CONTACT)
        {
            RequestContact = true // This makes the button request the user's contact information
        };

        var keyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { contactButton } })
        {
            ResizeKeyboard = true
        };

        await _botClient.SendMessageAsync(chatId, contactMessage, replyMarkup: keyboard);
    }

    public static string GetUserLanguage(string chatId)
    {
        return UserLanguages.ContainsKey(chatId) ? UserLanguages[chatId] : "🇬🇧 English";  // Default to English
    }
}