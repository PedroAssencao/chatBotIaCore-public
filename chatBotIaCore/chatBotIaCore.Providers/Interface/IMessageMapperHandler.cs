using chatBotIaCore.Domain.Models.System;
using System.Text.Json;

namespace chatBotIaCore.Providers.Interface
{
    public interface IMessageMapperHandler
    {
        IncomingMessage? getIncomingMessageFromJsonDocument(JsonDocument document);
    }
}
