using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Infra.Interfaces;
using chatBotIaCore.Services.Interfaces.Base;

namespace chatBotIaCore.Services.Services
{
    public class ContactServices : BaseServices<Contact>, IContactServices
    {
        protected new readonly IContactInterface _repository;
        public ContactServices(IBaseInterface<Contact> repository, IContactInterface repositoryContact) : base(repository)
        {
            _repository = repositoryContact;
        }

        public async Task<Contact> contactExistIfNotCreate(IncomingMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "IncomingMessage request cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(request.SenderId))
            {
                throw new ArgumentException("SenderId cannot be null or empty.", nameof(request.SenderId));
            }

            if (string.IsNullOrWhiteSpace(request.Phone))
            {
                throw new ArgumentException("Phone cannot be null or empty.", nameof(request.Phone));
            }

            return await _repository.contactExistIfNotCreate(request);
        }

        public Task<bool> contactIsBlocked(string externalId)
        {
            if (string.IsNullOrWhiteSpace(externalId))
            {
                throw new ArgumentException("ExternalId cannot be null or empty.", nameof(externalId));
            }

            return _repository.contactIsBlocked(externalId);
        }

        public Task<bool> contactIsClient(string externalId)
        {
            if (string.IsNullOrWhiteSpace(externalId))
            {
                throw new ArgumentException("ExternalId cannot be null or empty.", nameof(externalId));
            }

            return _repository.contactIsClient(externalId);
        }
    }
}
