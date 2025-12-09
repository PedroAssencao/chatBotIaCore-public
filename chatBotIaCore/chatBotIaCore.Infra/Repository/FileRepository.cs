using chatBotIaCore.Domain.Models;
using chatBotIaCore.Infra.DAL;
using chatBotIaCore.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chatBotIaCore.Infra.Repository
{
    public class FileRepository : BaseRepository<FileModel>, IFileInterface
    {
        public FileRepository(ChatBotIaCoreContext context) : base(context)
        {
        }

        public async Task<FileModel?> getFileByPath(string path) => await _context.Set<FileModel>().FirstOrDefaultAsync(f => f.FilePath == path);
    }
}
