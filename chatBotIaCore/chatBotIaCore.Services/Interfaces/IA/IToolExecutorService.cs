namespace chatBotIaCore.Services.Interfaces.IA
{
    public interface IToolExecutorService
    {
        Task<string> ExecuteToolAsync(string toolName, string jsonArguments);
    }
}
