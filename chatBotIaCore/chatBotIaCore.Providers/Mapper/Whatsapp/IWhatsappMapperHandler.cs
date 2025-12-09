using chatBotIaCore.Domain.Models.System;
using System.Text.Json;

namespace chatBotIaCore.Providers.Mapper.Whatsapp
{
    public interface IWhatsappMapperHandler
    {
        IncomingMessage mapSampleRequestToIncomingMessage(JsonDocument document);
        IncomingMessage mapImageAttachedRequestToIncomingMessage(JsonDocument document);
        IncomingMessage mapDocumentAttachedRequestToIncomingMessage(JsonDocument document);
    }
}
