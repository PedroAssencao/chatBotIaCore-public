using chatBotIaCore.Infra.Interfaces;
using chatBotIaCore.Providers.Clients.IA;
using chatBotIaCore.Providers.Clients.Meta;
using chatBotIaCore.Providers.Factories.IA.OpenAI;
using chatBotIaCore.Providers.Factories.Meta;
using chatBotIaCore.Providers.Interface;
using chatBotIaCore.Providers.Interface.IA;
using chatBotIaCore.Providers.Mapper;
using chatBotIaCore.Providers.Mapper.IA;
using chatBotIaCore.Providers.Mapper.Whatsapp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace chatBotIaCore.Providers.Extensions
{
    public static class AddProviderExtensions
    {
        public static void addProviderExtensionsStartUp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IWhatsappMapperHandler, WhatsappMapperHandler>();
            services.AddScoped<IMessageMapperHandler, MessageMapperHandler>();
            services.AddScoped<IOpenAIMapper, OpenAIMapper>();
            services.AddScoped<IOpenAIClientFactory, OpenAIClientFactory>();
            services.AddScoped<IMetaClientFactory, MetaClientFactory>();
            services.AddScoped<IMessageProcessingHandler>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var MessageInterface = provider.GetRequiredService<IMessageInterface>();
                var metaClientFactoryInterface = provider.GetRequiredService<IMetaClientFactory>();
                var authorization = configuration["Meta:Authorization"] ?? "";
                return new MessageProcessingWhatsappHandler(authorization, configuration, MessageInterface, metaClientFactoryInterface);
            });
            services.AddScoped<IAiProvider>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var openAIInterface = provider.GetRequiredService<IOpenAIMapper>();
                var factoryClient = provider.GetRequiredService<IOpenAIClientFactory>();
                var authorization = configuration["IA:AuthorizationOpenAI"] ?? configuration["IA:AuthorizationGitHub"] ?? "";

                return new OpenAIProvider(authorization, openAIInterface, factoryClient);
            });
        }
    }
}
