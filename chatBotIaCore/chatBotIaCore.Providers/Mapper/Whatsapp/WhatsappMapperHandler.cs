using chatBotIaCore.Domain.Models.Json.Meta;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Providers.DTO.Whatsapp;
using Newtonsoft.Json;
using System.Text.Json;

namespace chatBotIaCore.Providers.Mapper.Whatsapp
{
    public class WhatsappMapperHandler : IWhatsappMapperHandler
    {
        public IncomingMessage mapSampleRequestToIncomingMessage(JsonDocument document)
        {
            var documentObject = JsonConvert.DeserializeObject<SampleMessageTemplate.Root>(document.RootElement.ToString());

            return new IncomingMessage
            {
                MessageId = documentObject?.entry?.FirstOrDefault()?.changes?.FirstOrDefault()?.value?.messages?.FirstOrDefault()?.id ?? "",
                Name = documentObject?.entry?.FirstOrDefault()?.changes?.FirstOrDefault()?.value?.contacts?.FirstOrDefault()?.profile?.name ?? "",
                Phone = documentObject?.entry?.FirstOrDefault()?.changes?.FirstOrDefault()?.value?.messages?.FirstOrDefault()?.from ?? "",
                SenderId = documentObject?.entry?.FirstOrDefault()?.changes?.FirstOrDefault()?.value?.contacts?.FirstOrDefault()?.wa_id ?? "",
                createdAt = DateTimeOffset.FromUnixTimeSeconds(long.Parse(documentObject?.entry?.FirstOrDefault()?.changes?.FirstOrDefault()?.value?.messages?.FirstOrDefault()?.timestamp ?? "0")).DateTime,
                MessageInput = documentObject?.entry?.FirstOrDefault()?.changes?.FirstOrDefault()?.value?.messages?.FirstOrDefault()?.text?.body ?? "",
                MessageType = EMessageType.contact,
                ChannelType = EChannelType.WhatsApp
            };
        }

        public IncomingMessage mapImageAttachedRequestToIncomingMessage(JsonDocument document)
        {
            var documentObject = JsonConvert.DeserializeObject<MessageWithImageAttachedSetJson.Root>(document.RootElement.ToString());

            var valueData = documentObject?.entry?.FirstOrDefault()?.changes?.FirstOrDefault()?.value;
            var messageData = valueData?.messages?.FirstOrDefault();
            var contactData = valueData?.contacts?.FirstOrDefault();

            return new IncomingMessage
            {
                MessageId = messageData?.id ?? "",
                Name = contactData?.profile?.name ?? "",
                Phone = messageData?.from ?? "",
                SenderId = contactData?.wa_id ?? "",

                createdAt = DateTimeOffset.FromUnixTimeSeconds(long.Parse(messageData?.timestamp ?? "0")).DateTime,

                MessageInput = messageData?.image?.caption ?? "",

                MessageType = EMessageType.contact,
                ChannelType = EChannelType.WhatsApp,

                File = mappeDocumentJsonRequestToFIleDTO(document)
            };
        }

        public IncomingMessage mapDocumentAttachedRequestToIncomingMessage(JsonDocument document)
        {
            var documentObject = JsonConvert.DeserializeObject<MessageWithDocumentAttachedSetJson.Root>(document.RootElement.ToString());

            var valueData = documentObject?.entry?.FirstOrDefault()?.changes?.FirstOrDefault()?.value;
            var messageData = valueData?.messages?.FirstOrDefault();
            var contactData = valueData?.contacts?.FirstOrDefault();

            return new IncomingMessage
            {
                MessageId = messageData?.id ?? "",
                Name = contactData?.profile?.name ?? "",
                Phone = messageData?.from ?? "",
                SenderId = contactData?.wa_id ?? "",

                createdAt = DateTimeOffset.FromUnixTimeSeconds(long.Parse(messageData?.timestamp ?? "0")).DateTime,

                MessageInput = messageData?.document?.caption ?? "",

                MessageType = EMessageType.contact,
                ChannelType = EChannelType.WhatsApp,

                File = mappeDocumentAttachedJsonRequestToFIleDTO(document)
            };
        }

        private fileModelDto mappeDocumentAttachedJsonRequestToFIleDTO(JsonDocument Document)
        {
            var documentObject = JsonConvert.DeserializeObject<MessageWithDocumentAttachedSetJson.Root>(Document.RootElement.ToString());
            var documentData = documentObject?.entry?.FirstOrDefault()?.changes?.FirstOrDefault()?.value?.messages?.FirstOrDefault()?.document;

            string extension = "";
            if (!string.IsNullOrEmpty(documentData?.filename) && documentData.filename.Contains("."))
            {
                extension = Path.GetExtension(documentData.filename);
            }
            else if (!string.IsNullOrEmpty(documentData?.mime_type) && documentData.mime_type.Contains("/"))
            {
                extension = "." + documentData.mime_type.Split('/')[1];
            }

            return new fileModelDto
            {
                id = documentData?.id ?? "",
                sha256 = documentData?.sha256 ?? "",
                fileType = EFileType.Document,
                fileExtension = extension,
                fileName = documentData?.filename ?? (documentData?.id ?? "documento_whatsapp")
            };
        }

        private fileModelDto mappeDocumentJsonRequestToFIleDTO(JsonDocument Document)
        {            
            var documentObject = JsonConvert.DeserializeObject<MessageWithImageAttachedSetJson.Root>(Document.RootElement.ToString());
            var imageData = documentObject?.entry?.FirstOrDefault()?.changes?.FirstOrDefault()?.value?.messages?.FirstOrDefault()?.image;
            string extension = "";
            if (!string.IsNullOrEmpty(imageData?.mime_type) && imageData.mime_type.Contains("/"))
            {
                extension = "." + imageData.mime_type.Split('/')[1].Replace("jpeg", "jpg");
            }

            return new fileModelDto
            {
                id = imageData?.id ?? "",
                sha256 = imageData?.sha256 ?? "",
                fileType = EFileType.Image,
                fileExtension = extension,
                fileName = !string.IsNullOrEmpty(imageData?.caption) ? imageData.caption : (imageData?.id ?? "imagem_whatsapp")
            };
        }
    }
}
