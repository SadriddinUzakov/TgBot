using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace Application.Handlers.FileHandler
{
    public class UploadExcelCommandHandler : ICommandHandler
    {
        private readonly TelegramBotClientWrapper _botClient;

        public UploadExcelCommandHandler(TelegramBotClientWrapper botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleAsync(string chatId)
        {
            await _botClient.SendMessageAsync(chatId, "Please upload an Excel file.");
        }
    }
}
