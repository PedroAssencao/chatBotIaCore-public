using chatBotIaCore.Services.DTO;
using System.Text.Json;

namespace chatBotIaCore.Services.Interfaces.Meta
{
    public interface IMessageProcessing
    {
        Task<ResponseModel> ProcessingMessage(JsonDocument document);
    }
}
