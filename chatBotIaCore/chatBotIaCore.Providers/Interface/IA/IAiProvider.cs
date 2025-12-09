using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Providers.DTO.IA;

namespace chatBotIaCore.Providers.Interface.IA
{
    public interface IAiProvider
    {
        public EProviderType providerType { get; }
        Task<LlmResponse> getComplemetations(List<Message> historyChat, BotConfiguration settings, string? chatHistory = "");
        Task<string> generateChatSummary(List<Message> historyChat, BotConfiguration settings, string? chatHistory = "");
        Task<string> extractImageContext(byte[] imageBytes, BotConfiguration? settings = null);
        Task<string> transcribleAudio(byte[] audioBytes);
    }
}
