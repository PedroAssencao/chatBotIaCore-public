using chatBotIaCore.Domain.Models;
using OpenAI.Chat;

namespace chatBotIaCore.Providers.Mapper.IA
{
    public interface IOpenAIMapper
    {
        List<ChatMessage> mappeBotConfigurationToChatMassage(BotConfiguration settings, List<Message> message, string? chatHistory = "");
        ChatCompletionOptions mappeBotConfigurationToChatCompletionOptions(BotConfiguration settings);
    }
}
