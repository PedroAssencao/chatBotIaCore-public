using chatBotIaCore.API.Mapper.UserMapper;
using chatBotIaCore.Infra.DAL;
using chatBotIaCore.Infra.Extensions;
using chatBotIaCore.Providers.Extensions;
using chatBotIaCore.Services.Extensions;
using chatBotIaCore.Storage.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace chatBotIaCore.API.Extensions
{
    public static class StartUp
    {
        public static void startConfiguration(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.startConfiguration();
            services.ConfigurateInfraServices(configuration);
            services.ConfigurateLayerServices(configuration);
            services.ConfigureMapping();
        }

        public static void ConfigureMapping(this IServiceCollection services)
        {
            services.AddScoped<IUserMapper, UserMapper>();
        }

        public static void ConfigurateInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.addInfraExtensionsStartUp(configuration);
            services.addProviderExtensionsStartUp(configuration);
            services.addStorageExtensionsStartUp(configuration);
        }

        public static void ConfigurateLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.addServicesExtensionsStartUp(configuration);
        }

        public static void Configure(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            var arquivosPath = "/app/Files";

            if (!Directory.Exists(arquivosPath))
            {
                Directory.CreateDirectory(arquivosPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(arquivosPath),
                RequestPath = "/app/Files",
                ServeUnknownFileTypes = true,
            });

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ChatBotIaCoreContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            app.UseHttpsRedirection();

            app.MapControllers();
        }
    }
}
