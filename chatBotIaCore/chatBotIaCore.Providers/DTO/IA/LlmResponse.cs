using chatBotIaCore.Domain.Types;

namespace chatBotIaCore.Providers.DTO.IA
{
    public class LlmResponse
    {
        public string textResponse { get; set; } = string.Empty;
        public string toolToUse { get; set; } = string.Empty;
        public string toolArguments { get; set; } = "{}";
        public bool generateFile { get; set; } = false;
        public EChatType? chatType { get; set; } = null;
        public static LlmResponse failure(string? message = "Não informado")
        {
            return new LlmResponse
            {
                textResponse = "Ocorreu um error ao tentar gerar a resposta, error: " + message,
                toolToUse = "",
                chatType = EChatType.Ongoing,
            };
        }
        public static string SanitizeLlmResponse(string rawText)
        {
            if (string.IsNullOrEmpty(rawText))
            {
                return string.Empty;
            }

            string cleanedText = rawText.Replace("```json", "", StringComparison.OrdinalIgnoreCase)
                                       .Replace("```", "", StringComparison.OrdinalIgnoreCase)
                                       .Trim();

            int firstBrace = cleanedText.IndexOf('{');
            int lastBrace = cleanedText.LastIndexOf('}');

            if (firstBrace >= 0 && lastBrace > firstBrace)
            {
                return cleanedText.Substring(firstBrace, lastBrace - firstBrace + 1);
            }

            return string.Empty;
        }
        public static object assemblaImageRequestObject(string base64Image)
        {

            var requestBody = new
            {
                model = "gpt-5-nano",
                messages = new[]
                    {
                        new
                        {
                            role = "user",
                            content = new object[]
                            {
                                new { type = "text", text = "Transcreva o texto nesta imagem. Se não houver texto, retorne 'No text in this image'. Se possível, analise a imagem para deixar a infromação sobre ela mais rica do que apenas o texto como um todo." },
                                new { type = "image_url", image_url = new { url = $"data:image/jpeg;base64,{base64Image}" } }
                            }
                        }
                    }
            };

            return requestBody;
        }
    }
}
