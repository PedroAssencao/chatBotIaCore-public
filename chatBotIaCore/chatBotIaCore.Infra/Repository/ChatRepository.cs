using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Infra.DAL;
using chatBotIaCore.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chatBotIaCore.Infra.Repository
{
    public class ChatRepository : BaseRepository<Chat>, IChatInterface
    {
        public ChatRepository(ChatBotIaCoreContext context) : base(context)
        {
        }

        public async Task<Chat> chatExistIfNotCreate(IncomingMessage model)
        {
            Chat? chat = await _context.Chats.Include(x => x.Con).Include(x => x.Messages)
            .Include(x => x.Use).FirstOrDefaultAsync(x => x.ConId == model.Contact.ConId);
            if (chat == null)
            {
                chat = new Chat
                {
                    ConId = model.Contact.ConId,
                    ChaStatus = EChatType.Ongoing,
                    ChaCreatedAt = DateTime.Now,
                    ChaUpdatedAt = DateTime.Now
                };
                var result = await _context.Chats.AddAsync(chat);
                chat.ChaId = result.Entity.ChaId;
                await _context.SaveChangesAsync();
            }

            return chat;
        }

        public async Task<Chat?> getChatByContactId(int id) => await _context.Chats
            .Include(x => x.Messages)
            .Include(x => x.Use)
            .Include(x => x.Con)
            .FirstOrDefaultAsync(x => x.ConId == id);
    }
}
