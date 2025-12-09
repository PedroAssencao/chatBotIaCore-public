using chatBotIaCore.Domain.Models;
using chatBotIaCore.Infra.Interfaces;
using chatBotIaCore.Services.Interfaces.Base;

namespace chatBotIaCore.Services.Services
{
    public class BotConfigurationServices : BaseServices<BotConfiguration>, IBotConfigurationServices
    {
        protected new readonly IBotConfigurationInterface _repository;
        public BotConfigurationServices(IBaseInterface<BotConfiguration> repository, IBotConfigurationInterface repo) : base(repository)
        {
            _repository = repo;
        }

        public async Task<BotConfiguration?> getTheActiveBotAsync() => await _repository.getTheActiveBotAsync();
    }
}
