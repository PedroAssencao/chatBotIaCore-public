using chatBotIaCore.Domain.Models;

namespace chatBotIaCore.Services.Interfaces.Base
{
    public interface IBotConfigurationServices : IBaseServices<BotConfiguration>
    {
        Task<BotConfiguration?> getTheActiveBotAsync();
    }
}
