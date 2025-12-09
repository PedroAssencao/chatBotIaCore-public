using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Infra.Interfaces;
using chatBotIaCore.Providers.Interface;
using chatBotIaCore.Services.Interfaces.Base;
using Microsoft.IdentityModel.Tokens;

namespace chatBotIaCore.Services.Services
{
    public class MessageServices : BaseServices<Message>, IMessageServices
    {
        protected new readonly IMessageInterface _repository;
        protected readonly IMessageProcessingHandler _MessageProcessingHandler;
        public MessageServices(IBaseInterface<Message> repository, IMessageInterface messageInterface, IMessageProcessingHandler messageHandler) : base(repository)
        {
            _repository = messageInterface;
            _MessageProcessingHandler = messageHandler;
        }

        public async Task<bool> messageAlreadyExist(string externalId)
        {

            if (externalId.IsNullOrEmpty())
            {
                throw new Exception("Id is mandatory");
            }

            return await _repository.messageAlreadyExist(externalId);
        }
        public async Task<bool> ensureMessageContextAndSave(IncomingMessage request)
        {
            var messageState = await messageAlreadyExist(request.MessageId ?? "");
            if (messageState) return messageState;

            await createAsync(Message.requestModelToMessage(request));
            return messageState;
        }
    }
}
