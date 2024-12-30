using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace Application.Handlers.FileHandler
{
    public class UploadPowerPointCommandHandler : ICommandHandler
    {
        private readonly TelegramBotClientWrapper _botClient;

        public UploadPowerPointCommandHandler(TelegramBotClientWrapper botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleAsync(string chatId)
        {
            await _botClient.SendMessageAsync(chatId, "Please upload a PowerPoint file.");
        }
    }
}
