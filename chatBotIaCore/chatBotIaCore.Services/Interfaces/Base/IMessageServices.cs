using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;

namespace chatBotIaCore.Services.Interfaces.Base
{
    public interface IMessageServices : IBaseServices<Message>
    {
        Task<bool> messageAlreadyExist(string externalId);
        Task<bool> ensureMessageContextAndSave(IncomingMessage request);
    }
}
