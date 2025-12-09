using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Providers.DTO.IA;
using chatBotIaCore.Providers.Interface.IA;
using chatBotIaCore.Services.DTO;
using chatBotIaCore.Services.Interfaces.Base;
using chatBotIaCore.Services.Interfaces.IA;

namespace chatBotIaCore.Services.Core
{
    public class ChatOrchestrationService : IChatOrchestrationService
    {
        protected readonly IEnumerable<IAiProvider> _AiProvider;
        protected readonly IBotConfigurationServices _botServices;
        protected IAiProvider? _AIClient;
        protected readonly IFileManagerServices _FileManagerServices;
        protected readonly IToolExecutorService _toolExecutor;

        public ChatOrchestrationService(IEnumerable<IAiProvider> aiProvider,
            IBotConfigurationServices botConfiguration,
            IFileManagerServices fileManagerServices,
            IToolExecutorService toolExecutor)
        {
            _AiProvider = aiProvider;
            _botServices = botConfiguration;
            _FileManagerServices = fileManagerServices;
            _toolExecutor = toolExecutor;
        }

        public async Task<OrchestrationResult> ProcessMessageFlowAsync(IncomingMessage request, Func<string, Task>? onStatusUpdate = null)
        {
            _AIClient = _AiProvider.First(x => x.providerType == EProviderType.OpenAI);
            BotConfiguration? configuration = await _botServices.getTheActiveBotAsync();
            BotConfiguration.checkIfAllBotsAreDeactived(configuration);

            if (!request.File.id.Equals(""))
            {
                await _FileManagerServices.processFileAsync(request);
            }

            LlmResponse result = await _AIClient.getComplemetations(request.Chat.Messages.ToList(), configuration!, request.Chat.ChaHistory);
            OrchestrationResult response = OrchestrationResult.AssemblyOrcherStrarionResultResponse(result);

            if (!string.IsNullOrEmpty(result.toolToUse))
            {
                if (onStatusUpdate != null && !string.IsNullOrEmpty(result.textResponse))
                {
                    await onStatusUpdate(result.textResponse);
                }

                response.AssemblyOrcherStrarionResultResponseWithTool(result, await _toolExecutor.ExecuteToolAsync(result.toolToUse, result.toolArguments), request);
            }

            request.Chat.ChaHistory = await _AIClient.generateChatSummary(request.Chat.Messages.ToList(), configuration!.returnBotConfigurationToChatSumarry(), request.Chat.ChaHistory);
            if (result.chatType.HasValue) request.Chat.ChaStatus = result.chatType.Value;

            return response;
        }
    }
}
