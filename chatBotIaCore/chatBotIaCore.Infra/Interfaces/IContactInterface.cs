using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;

namespace chatBotIaCore.Infra.Interfaces
{
    public interface IContactInterface
    {
        Task<bool> contactIsBlocked(string externalId);
        Task<bool> contactIsClient(string externalId);
        Task<Contact> contactExistIfNotCreate(IncomingMessage request);
    }
}
