using chatBotIaCore.Domain.Models;
using chatBotIaCore.Infra.DAL;
using chatBotIaCore.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chatBotIaCore.Infra.Repository
{
    public class BotConfigurationRepository : BaseRepository<BotConfiguration>, IBotConfigurationInterface
    {
        public BotConfigurationRepository(ChatBotIaCoreContext context) : base(context)
        {
        }

        public async Task<BotConfiguration?> getTheActiveBotAsync()
        {
            BotConfiguration? botConfiguration = await _context.BotConfigurations.AsNoTracking()
            .Include(x => x.UseCases)
            .ThenInclude(x => x.UseCaseToolMappings)
            .ThenInclude(x => x.Tool)
            .ThenInclude(x => x.ToolParameters)
            .FirstOrDefaultAsync(x => x.BotSystemEnabled);

            if (botConfiguration != null)
            {
                botConfiguration.returnToolsAndToolsDefinition();
            }

            return botConfiguration;
        }
    }
}
