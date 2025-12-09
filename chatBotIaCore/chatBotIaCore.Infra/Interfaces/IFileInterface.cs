using chatBotIaCore.Domain.Models;

namespace chatBotIaCore.Infra.Interfaces
{
    public interface IFileInterface : IBaseInterface<FileModel>
    {
        Task<FileModel?> getFileByPath(string path);
    }
}
