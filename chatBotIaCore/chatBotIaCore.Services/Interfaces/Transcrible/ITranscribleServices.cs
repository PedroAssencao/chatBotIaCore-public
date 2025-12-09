using chatBotIaCore.Domain.Models;

namespace chatBotIaCore.Services.Interfaces.Transcrible
{
    public interface ITranscribleServices
    {
        Task transcribleDocument(List<Message> messages);
    }
}
