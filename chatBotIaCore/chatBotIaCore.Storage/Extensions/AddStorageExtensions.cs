using chatBotIaCore.Infra.Interfaces;
using chatBotIaCore.Storage.Interface;
using chatBotIaCore.Storage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace chatBotIaCore.Storage.Extensions
{
    public static class AddStorageExtensions
    {
        public static void addStorageExtensionsStartUp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStorageServices>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var localHost = configuration["Host"] ?? "";
                return new DiskStorageServices(localHost);
            });
            services.AddScoped<IConvertionServices, ConvertionServices>();
        }
    }
}
