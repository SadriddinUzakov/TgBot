using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace Application.Handlers
{
    public class HelpCommandHandler : ICommandHandler
    {
        private readonly TelegramBotClientWrapper _botClient;

        public HelpCommandHandler(TelegramBotClientWrapper botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleAsync(string chatId)
        {
            await _botClient.SendMessageAsync(chatId, "To Get Information about bot :\n/info"+
                "To start contact with admins:\n/admins");
        }
    }
}
