using chatBotIaCore.Domain.Models;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace chatBotIaCore.Providers.Factories.IA.OpenAI
{
    public class OpenAIClientFactory : IOpenAIClientFactory
    {
        public ChatClient CreateClient(string apiKey, BotConfiguration settings)
        {
            bool isGitHubClient = apiKey.Length == 40;

            if (isGitHubClient)
            {
                var openAIOptions = new OpenAIClientOptions()
                {
                    Endpoint = new Uri("https://models.github.ai/inference")
                };

                return new ChatClient(
                    model: settings.BotModelName,
                    credential: new ApiKeyCredential(apiKey),
                    options: openAIOptions
                );
            }

            return new ChatClient(
                model: settings.BotModelName,
                apiKey: apiKey
            );
        }

        public HttpClient CreateHttpClient(string apiKey, BotConfiguration? settings = null)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            return client;
        }
    }
}
