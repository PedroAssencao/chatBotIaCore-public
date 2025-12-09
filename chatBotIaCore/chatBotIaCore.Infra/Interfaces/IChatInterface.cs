using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;

namespace chatBotIaCore.Infra.Interfaces
{
    public interface IChatInterface
    {
        Task<Chat?> getChatByContactId(int id);
        Task<Chat> chatExistIfNotCreate(IncomingMessage model);
    }
}
