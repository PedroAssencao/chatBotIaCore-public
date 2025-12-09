using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Types;
using OpenAI.Chat;

namespace chatBotIaCore.Providers.Mapper.IA
{
    public class OpenAIMapper : IOpenAIMapper
    {
        public ChatCompletionOptions mappeBotConfigurationToChatCompletionOptions(BotConfiguration settings) => new ChatCompletionOptions
        {
            Temperature = (float)Convert.ToDouble(settings.BotTemperature),
            MaxOutputTokenCount = settings.BotMaxOutputTokenCount,
            ResponseFormat = settings.BotForceJsonResponse ?
                ChatResponseFormat.CreateJsonSchemaFormat(
                        jsonSchemaFormatName: "LlmResponse",
                        jsonSchema: BinaryData.FromString(settings.BotSystemPromptJsonResponse),
                        jsonSchemaIsStrict: true) : null
        };

        public List<ChatMessage> mappeBotConfigurationToChatMassage(BotConfiguration settings, List<Message> message, string? chatHistory = "")
        {
            List<ChatMessage> chatMessages = new List<ChatMessage> { new SystemChatMessage(settings.BotSystemPrompt) };
            chatMessages.Add(new SystemChatMessage("RESUMO DA CONVERSA ANTERIOR PARA CONTEXTO: \n" + chatHistory));
            foreach (var x in message.OrderByDescending(x => x.MesId).Take(20).OrderBy(x => x.MesId))
            {
                try
                {
                    dynamic? chatMessageType = null;

                    if (EMessageType.assistant == x.MesRole)
                    {
                        chatMessageType = new AssistantChatMessage(x.ReturnMessageToRequest());
                    }

                    if (EMessageType.contact == x.MesRole)
                    {
                        chatMessageType = new UserChatMessage(x.ReturnMessageToRequest());
                    }

                    if (EMessageType.tool == x.MesRole)
                    {
                        chatMessageType = new ToolChatMessage(x.ReturnMessageToRequest());
                    }

                    chatMessages.Add(chatMessageType);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            if (settings.BotSystemPrompt.Contains("FollowALLTHEROLESTHATAREINTHISPROMPT")) chatMessages.Add(new UserChatMessage("Gere o resumo da conversa inteira com base nas instruções do System Prompt."));

            return chatMessages;
        }
    }
}
