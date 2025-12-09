using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Services.Interfaces.IA;
using System.Reflection;
using System.Text.Json;

namespace chatBotIaCore.Services.Services
{
    public class ToolExecutorService : IToolExecutorService
    {
        private readonly IServiceProvider _serviceProvider;

        public ToolExecutorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<string> ExecuteToolAsync(string toolName, string jsonArguments)
        {
            var method = Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods())
                .FirstOrDefault(m => m.GetCustomAttribute<AI_ToolAttribute>()?.Name == toolName);

            if (method == null)
                return $"Error: Tool '{toolName}' implementation not found in code.";

            var serviceType = method.DeclaringType;
            var serviceInstance = _serviceProvider.GetService(serviceType!);

            if (serviceInstance == null)
                return $"Error: Service {serviceType!.Name} is not registered in DI container.";

            var parameters = method.GetParameters();
            var argsArray = new object[parameters.Length];

            try
            {
                using var document = JsonDocument.Parse(jsonArguments);
                var root = document.RootElement;

                for (int i = 0; i < parameters.Length; i++)
                {
                    var param = parameters[i];
                    if (root.TryGetProperty(param.Name ?? "", out var jsonElement))
                    {
                        argsArray[i] = JsonSerializer.Deserialize(jsonElement.GetRawText(), param.ParameterType) ?? "";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error parsing arguments: {ex.Message}";
            }
            
            try
            {
                var result = method.Invoke(serviceInstance, argsArray);

                if (result is Task task)
                {
                    await task.ConfigureAwait(false);
                    var resultProperty = task.GetType().GetProperty("Result");
                    return resultProperty?.GetValue(task)?.ToString() ?? "Done";
                }

                return result?.ToString() ?? "Done";
            }
            catch (Exception ex)
            {
                return $"Error executing tool: {ex.InnerException?.Message ?? ex.Message}";
            }
        }
    }
}
