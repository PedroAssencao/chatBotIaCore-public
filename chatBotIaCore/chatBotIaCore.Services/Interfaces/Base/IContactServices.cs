using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;

namespace chatBotIaCore.Services.Interfaces.Base
{
    public interface IContactServices : IBaseServices<Contact>
    {
        Task<bool> contactIsBlocked(string externalId);
        Task<bool> contactIsClient(string externalId);
        Task<Contact> contactExistIfNotCreate(IncomingMessage request);
    }
}
