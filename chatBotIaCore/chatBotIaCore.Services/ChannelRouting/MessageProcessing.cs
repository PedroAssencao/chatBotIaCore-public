using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Providers.Interface;
using chatBotIaCore.Services.DTO;
using chatBotIaCore.Services.Interfaces.Base;
using chatBotIaCore.Services.Interfaces.IA;
using chatBotIaCore.Services.Interfaces.Meta;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace chatBotIaCore.Services.ChannelRouting
{
    public class MessageProcessing : IMessageProcessing
    {
        protected readonly IMessageServices _MessageServices;
        protected readonly IMessageMapperHandler _Mapper;
        protected readonly IContactServices _ContactServices;
        protected readonly IChatServices _ChatServices;
        protected readonly IEnumerable<IMessageProcessingHandler> _channelProviderHandler;
        protected IMessageProcessingHandler? _chnanelHandler;
        protected readonly IChatOrchestrationService _OchersateCoreServices;

        public MessageProcessing(
            IMessageServices messageServices,
            IMessageMapperHandler mapper,
            IContactServices contactServices,
            IEnumerable<IMessageProcessingHandler> channelProviderHandler,
            IChatServices chatServices,
            IChatOrchestrationService orchestrationService)
        {
            _MessageServices = messageServices;
            _Mapper = mapper;
            _ContactServices = contactServices;
            _channelProviderHandler = channelProviderHandler;
            _ChatServices = chatServices;
            _OchersateCoreServices = orchestrationService;
        }

        public async Task<ResponseModel> ProcessingMessage(JsonDocument document)
        {
            ResponseModel responseModel = new ResponseModel();
            IncomingMessage? request = _Mapper.getIncomingMessageFromJsonDocument(document);

            if (request is null) return responseModel.failure();
            _chnanelHandler = _channelProviderHandler.First(x => x.ChannelHandler == request.ChannelType);

            await ensureChatAndContactExist(request);
            if (await _MessageServices.ensureMessageContextAndSave(request)) return responseModel.failure();

            try
            {
                string contactValidate = request.Contact!.contactValidate();
                if (!contactValidate.IsNullOrEmpty())
                {
                    await _chnanelHandler.sendMessageAsync(request.MappeRequestClientToRequestServer(contactValidate));
                    return responseModel.failure();
                }

                OrchestrationResult result;
                try
                {
                    result = await _OchersateCoreServices.ProcessMessageFlowAsync(request, async (content) =>
                    {
                        await _chnanelHandler!.sendMessageAsync(request.MappeRequestClientToRequestServer(content));
                    });
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentNullException) return responseModel.failure(ex.Message);
                    throw;
                }

                string messageExternalId = await responseActionHandler(result, request);

                return responseModel.success(request.MessageId, messageExternalId);
            }
            catch (Exception ex)
            {
                await _chnanelHandler.sendMessageAsync(request.MappeRequestClientToRequestServer("There was a error to generate the response, error: " + ex.Message));
                return responseModel.failure();
            }
        }

        private async Task<string> responseActionHandler(OrchestrationResult result, IncomingMessage request)
        {            
            var externalMessageId = string.Empty;

            if (result.ShouldUpdateChatStatus) await _ChatServices.updateAsync(request.Chat);

            if (result.ActionType == ResponseActionType.StandardReply || result.ActionType == ResponseActionType.ToolGeneratedFile) externalMessageId = await _chnanelHandler!.sendMessageAsync(request.MappeRequestClientToRequestServer(result.ResponseText));         

            return externalMessageId;
        }
        private async Task ensureChatAndContactExist(IncomingMessage request)
        {
            request.Contact = await _ContactServices.contactExistIfNotCreate(request);
            request.Chat = await _ChatServices.getChatByContactId(request.Contact.ConId) ?? await _ChatServices.chatExistIfNotCreate(request);
        }
    }
}
