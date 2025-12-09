using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Infra.Interfaces;
using chatBotIaCore.Services.Interfaces.Base;

namespace chatBotIaCore.Services.Services
{
    public class ChatServices : BaseServices<Chat>, IChatServices
    {
        protected new readonly IChatInterface _repository;
        public ChatServices(IBaseInterface<Chat> repository, IChatInterface repositoryIN) : base(repository)
        {
            _repository = repositoryIN;
        }

        public Task<Chat> chatExistIfNotCreate(IncomingMessage model)
        {
            if (!model.checkifContactIsValid())
            {
                throw new ArgumentNullException("The contact's id is mandatory");
            }

            return _repository.chatExistIfNotCreate(model);
        }

        public async Task<Chat?> getChatByContactId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException("The id's mandatory");
            }

            return await _repository.getChatByContactId(id);
        }
    }
}
