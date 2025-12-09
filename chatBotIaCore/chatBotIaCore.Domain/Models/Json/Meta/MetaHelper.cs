namespace chatBotIaCore.Domain.Models.Json.Meta
{
    public class MetaHelper
    {
        public static string formatUrl(string baseUrl, string phoneNumberId, string version, string endpoint) => (baseUrl + $"/{endpoint}").Replace("{{Version}}", version).Replace("{{Phone-Number-ID}}", phoneNumberId);
    }
}
