using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Providers.DTO.IA;
using chatBotIaCore.Providers.Factories.IA.OpenAI;
using chatBotIaCore.Providers.Interface.IA;
using chatBotIaCore.Providers.Mapper.IA;
using Newtonsoft.Json;
using OpenAI.Audio;
using OpenAI.Chat;
using System.ClientModel;
using System.Text;

namespace chatBotIaCore.Providers.Clients.IA
{
    public class OpenAIProvider : IAiProvider
    {
        protected readonly string _apiKey;
        protected ChatClient? _chatClient;
        protected readonly IOpenAIMapper _openAIMapper;
        protected readonly IOpenAIClientFactory _clientFactory;
        public OpenAIProvider(string apiKey, IOpenAIMapper openAIMapper, IOpenAIClientFactory factory)
        {
            _apiKey = apiKey;
            _openAIMapper = openAIMapper;
            _clientFactory = factory;
        }
        public EProviderType providerType { get => EProviderType.OpenAI; }
        public async Task<LlmResponse> getComplemetations(List<Message> historyChat, BotConfiguration settings, string? chatHistory = "")
        {
            _chatClient = _clientFactory.CreateClient(_apiKey, settings);
            ClientResult<ChatCompletion> request;
            try
            {
                request = await _chatClient
                .CompleteChatAsync(
                _openAIMapper.mappeBotConfigurationToChatMassage(settings, historyChat, chatHistory),
                _openAIMapper.mappeBotConfigurationToChatCompletionOptions(settings)
                );
            }
            catch (Exception ex)
            {
                return LlmResponse.failure(ex.Message);
            }

            return JsonConvert.DeserializeObject<LlmResponse>(request?.Value?.Content[0]?.Text.ToString() ?? "") ?? throw new Exception("There was a problem to convert the return to json");
        }
        public async Task<string> generateChatSummary(List<Message> historyChat, BotConfiguration settings, string? chatHistory = "")
        {
            _chatClient = _clientFactory.CreateClient(_apiKey, settings);
            ClientResult<ChatCompletion> request;
            try
            {
                request = await _chatClient.CompleteChatAsync(
                _openAIMapper.mappeBotConfigurationToChatMassage(settings, historyChat, chatHistory),
                _openAIMapper.mappeBotConfigurationToChatCompletionOptions(settings)
                );
            }
            catch (Exception ex)
            {
                return LlmResponse.failure(ex.Message).textResponse;
            }

            return request.Value.Content[0].Text;
        }

        public async Task<string> extractImageContext(byte[] imageBytes, BotConfiguration? settings = null)
        {
            var httpClient = _clientFactory.CreateHttpClient(_apiKey, settings);

            var base64Image = Convert.ToBase64String(imageBytes);

            var content = new StringContent(JsonConvert.SerializeObject(LlmResponse.assemblaImageRequestObject(base64Image)), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseString);
                return responseObject?.choices[0].message.content ?? "";
            }
            else
            {
                throw new Exception($"Erro in the requisition: {response.StatusCode} - {responseString}");
            }
        }

        public async Task<string> transcribleAudio(byte[] audioBytes)
        {
            AudioClient client = new AudioClient("whisper-1", _apiKey);

            AudioTranscriptionOptions options = new AudioTranscriptionOptions()
            {
                ResponseFormat = AudioTranscriptionFormat.Verbose,
                TimestampGranularities = AudioTimestampGranularities.Word | AudioTimestampGranularities.Segment,
            };

            string transcriptionString = string.Empty;

            using (MemoryStream stream = new MemoryStream(audioBytes))
            {
                AudioTranscription transcription = await client.TranscribeAudioAsync(stream, "audio.mp3", options);
                transcriptionString += $"{transcription.Text} ";
            }
            return transcriptionString;
        }
    }
}
