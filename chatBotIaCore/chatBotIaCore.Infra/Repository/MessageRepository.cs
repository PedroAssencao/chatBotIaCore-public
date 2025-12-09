using chatBotIaCore.Domain.Models;
using chatBotIaCore.Infra.DAL;
using chatBotIaCore.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chatBotIaCore.Infra.Repository
{
    public class MessageRepository : BaseRepository<Message>, IMessageInterface
    {
        public MessageRepository(ChatBotIaCoreContext context) : base(context)
        {
        }

        public async Task<bool> messageAlreadyExist(string externalId) => await _context
            .Messages
            .Include(x => x.File)
            .AnyAsync(x => x.MesExternalId == externalId);
    }
}
