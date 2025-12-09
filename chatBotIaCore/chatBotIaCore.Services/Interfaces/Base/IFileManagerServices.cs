using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Infra.Interfaces;

namespace chatBotIaCore.Services.Interfaces.Base
{
    public interface IFileManagerServices : IBaseInterface<FileModel>
    {
        Task<FileModel?> getFileAsync(string path);
        Task<FileModel> processFileAsync(IncomingMessage request);
    }
}
