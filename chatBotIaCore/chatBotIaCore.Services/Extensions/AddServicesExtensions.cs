using chatBotIaCore.Services.ChannelRouting;
using chatBotIaCore.Services.Core;
using chatBotIaCore.Services.Interfaces.Base;
using chatBotIaCore.Services.Interfaces.IA;
using chatBotIaCore.Services.Interfaces.Meta;
using chatBotIaCore.Services.Interfaces.Transcrible;
using chatBotIaCore.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace chatBotIaCore.Services.Extensions
{
    public static class AddServicesExtensions
    {
        public static void addServicesExtensionsStartUp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IBaseServices<>), typeof(BaseServices<>));
            services.AddScoped<IMessageProcessing, MessageProcessing>();
            services.AddScoped<FileManagerServices>();
            services.AddScoped<IFileManagerServices>(provider => provider.GetRequiredService<FileManagerServices>());
            services.AddScoped<ITranscribleServices, TranscribleServices>();
            services.AddScoped<IMessageServices, MessageServices>();
            services.AddScoped<IToolExecutorService, ToolExecutorService>();
            services.AddScoped<IBotConfigurationServices, BotConfigurationServices>();
            services.AddScoped<IContactServices, ContactServices>();
            services.AddScoped<IChatOrchestrationService, ChatOrchestrationService>();
            services.AddScoped<IChatServices, ChatServices>();
        }
    }
}
