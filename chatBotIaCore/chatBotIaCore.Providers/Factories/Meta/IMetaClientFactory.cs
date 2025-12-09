namespace chatBotIaCore.Providers.Factories.Meta
{
    public interface IMetaClientFactory
    {
        HttpClient CreateClient(string apiKey);
    }
}
