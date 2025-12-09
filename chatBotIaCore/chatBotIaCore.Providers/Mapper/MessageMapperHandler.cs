using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Providers.Interface;
using chatBotIaCore.Providers.Mapper.Whatsapp;
using System.Text.Json;

namespace chatBotIaCore.Providers.Mapper
{
    public class MessageMapperHandler : IMessageMapperHandler
    {
        protected readonly IWhatsappMapperHandler _WhatsappHandler;

        public MessageMapperHandler(IWhatsappMapperHandler whatsappHandler)
        {
            _WhatsappHandler = whatsappHandler;
        }

        public IncomingMessage? getIncomingMessageFromJsonDocument(JsonDocument document)
        {
            var responseString = document.RootElement.ToString().Trim().ToLower();
            IncomingMessage? request = null;

            if (responseString.Contains("\"messaging_product\":\"whatsapp\"")) //i'll change this reconginiton type latter.
            {
                if (responseString.Contains("\"text\":{".Trim().ToLower()))
                {
                    request = _WhatsappHandler.mapSampleRequestToIncomingMessage(document);
                }

                if (responseString.Contains("\"type\":\"image\",\"image\":{".Trim().ToLower()))
                {
                    request = _WhatsappHandler.mapImageAttachedRequestToIncomingMessage(document);
                }

                if (responseString.Contains("\"type\":\"document\",\r\n\"document\":{".Trim().ToLower()))
                {
                    request = _WhatsappHandler.mapImageAttachedRequestToIncomingMessage(document);
                }
            }

            return request;
        }
    }
}
