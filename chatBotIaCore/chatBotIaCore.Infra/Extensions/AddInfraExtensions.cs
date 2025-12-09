using chatBotIaCore.Infra.DAL;
using chatBotIaCore.Infra.Interfaces;
using chatBotIaCore.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace chatBotIaCore.Infra.Extensions
{
    public static class AddInfraExtensions
    {
        public static void addInfraExtensionsStartUp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChatBotIaCoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Chinook"));
            });
            services.AddScoped(typeof(IBaseInterface<>), typeof(BaseRepository<>));
            services.AddScoped<IMessageInterface, MessageRepository>();
            services.AddScoped<IBotConfigurationInterface, BotConfigurationRepository>();
            services.AddScoped<IFileInterface, FileRepository>();
            services.AddScoped<IContactInterface, ContactReposiotry>();
            services.AddScoped<IChatInterface, ChatRepository>();
        }
    }
}
