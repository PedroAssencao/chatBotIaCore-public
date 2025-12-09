using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.Json.Meta;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Infra.Interfaces;
using chatBotIaCore.Providers.Factories.Meta;
using chatBotIaCore.Providers.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace chatBotIaCore.Providers.Clients.Meta
{
    public class MessageProcessingWhatsappHandler : IMessageProcessingHandler
    {
        protected readonly HttpClient _httpClient;
        protected readonly IMessageInterface _messageRepository;
        protected readonly string _authorization;
        protected readonly string _baseUrl;
        protected readonly string _phoneNumberId;
        protected readonly string _version;
        public EChannelType ChannelHandler => EChannelType.WhatsApp;

        public MessageProcessingWhatsappHandler(string authorization,
            IConfiguration configuration,
            IMessageInterface messageInterface,
            IMetaClientFactory metaClientFactory)
        {
            _messageRepository = messageInterface;
            _httpClient = metaClientFactory.CreateClient(authorization);
            _authorization = authorization;
            _baseUrl = configuration["Meta:BaseRequestUrl"] ?? "";
            _phoneNumberId = configuration["Meta:PhoneNumberId"] ?? "";
            _version = configuration["Meta:Version"] ?? "";
        }

        public async Task<string> sendMessageAsync(IncomingMessage request)
        {
            try
            {
                Message message = Message.requestModelToMessage(request);
                SampleMessageSetJson.Root requestModel = SampleMessageSetJson.createSampleBodyRequest(request);

                string url = SampleMessageSetJson.formatUrl(_baseUrl, _phoneNumberId, _version, "{{Phone-Number-ID}}/messages");
                string result = await SendMessageHttpAsync(url, requestModel);
                message.MesExternalId = JsonConvert.DeserializeObject<MessageSenderSetJson.Root>(result)?.messages.FirstOrDefault()?.id ?? "";

                await _messageRepository.createAsync(message);

                return message.MesExternalId;
            }
            catch (Exception ex)
            {
                throw new Exception("A error ocurrer while trying to send the message and/or save the message in the database, error: " + ex.Message);
            }
        }

        public async Task<byte[]> getMidiaDownloadAsync(string url)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authorization);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");

            HttpResponseMessage fileResponse = await _httpClient.GetAsync(url);
            fileResponse.EnsureSuccessStatusCode();

            if (fileResponse?.Content?.Headers?.ContentType?.MediaType == "text/html")
            {
                string content = await fileResponse.Content.ReadAsStringAsync();
                throw new Exception("A resposta é uma página HTML: " + content);
            }

            return await fileResponse!.Content.ReadAsByteArrayAsync();
        }

        public async Task<string> getMidiaDownloadUrlAsync(string id)
        {

            string url = SampleMessageSetJson.formatUrl(_baseUrl, _phoneNumberId, _version, id);
            var result = await _httpClient.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string jsonResponse = await result.Content.ReadAsStringAsync();
            string fileUrl = Newtonsoft.Json.Linq.JObject.Parse(jsonResponse)["url"]?.ToString() ?? "";

            return fileUrl;
        }

        private async Task<string> SendMessageHttpAsync(string url, object model)
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(model);
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, httpContent);
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (Exception ex)
            {
                throw new Exception("It wasn't possible to send the request, error: " + ex.Message);
            }
        }
    }
}