namespace Application.Handlers
{
    public interface ICommandHandler
    {
        Task HandleAsync(string chatId,string userMessage);
    }
}
