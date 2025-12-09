namespace chatBotIaCore.Providers.Factories.Meta
{
    public class MetaClientFactory : IMetaClientFactory
    {
        protected readonly HttpClient _httpClient = new HttpClient();
        public HttpClient CreateClient(string apiKey)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Other");
            return _httpClient;
        }
    }
}
