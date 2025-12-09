using chatBotIaCore.Domain.Models;
using OpenAI.Chat;

namespace chatBotIaCore.Providers.Factories.IA.OpenAI
{
    public interface IOpenAIClientFactory
    {
        ChatClient CreateClient(string apiKey, BotConfiguration settings);
        HttpClient CreateHttpClient(string apiKey, BotConfiguration? settings = null);
    }
}
