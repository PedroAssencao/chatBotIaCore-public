using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;

namespace chatBotIaCore.Providers.Interface
{
    public interface IMessageProcessingHandler
    {
        EChannelType ChannelHandler { get; }
        Task<string> sendMessageAsync(IncomingMessage request);
        Task<string> getMidiaDownloadUrlAsync(string id);
        Task<byte[]> getMidiaDownloadAsync(string url);
    }
}
