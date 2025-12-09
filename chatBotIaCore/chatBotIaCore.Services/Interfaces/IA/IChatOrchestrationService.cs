using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Services.DTO;

namespace chatBotIaCore.Services.Interfaces.IA
{
    public interface IChatOrchestrationService
    {
        Task<OrchestrationResult> ProcessMessageFlowAsync(IncomingMessage request, Func<string, Task>? onStatusUpdate = null);
    }
}
