using chatBotIaCore.Domain.Models;

namespace chatBotIaCore.Infra.Interfaces
{
    public interface IBotConfigurationInterface : IBaseInterface<BotConfiguration>
    {
        Task<BotConfiguration?> getTheActiveBotAsync();
    }
}
