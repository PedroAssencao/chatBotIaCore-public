using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;

namespace chatBotIaCore.Services.Interfaces.Base
{
    public interface IChatServices : IBaseServices<Chat>
    {
        Task<Chat?> getChatByContactId(int id);
        Task<Chat> chatExistIfNotCreate(IncomingMessage model);
    }
}
