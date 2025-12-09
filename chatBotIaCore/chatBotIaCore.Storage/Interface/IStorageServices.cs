using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;

namespace chatBotIaCore.Storage.Interface
{
    public interface IStorageServices
    {
        public EStorageServiceType Type { get; }
        Task<FileModel> saveFileAsync(IncomingMessage request);
        Task<byte[]> GetFileLocal(string filePath);
    }
}
