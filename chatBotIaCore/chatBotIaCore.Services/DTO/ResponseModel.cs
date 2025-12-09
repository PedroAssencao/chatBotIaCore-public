namespace chatBotIaCore.Services.DTO
{
    public class ResponseModel
    {
        public bool MessagedSended { get; set; }
        public string? MessageId { get; set; }
        public string? MessageRecaiverId { get; set; }
        public string obs { get; set; } = string.Empty;

        public ResponseModel failure(string message = "")
        {
            this.MessagedSended = false;
            this.MessageId = "";
            this.MessageRecaiverId = "";
            this.obs = message;
            return this;
        }

        public ResponseModel success(string recaiverMessage, string senderMessager)
        {
            this.MessagedSended = true;
            this.MessageId = senderMessager;
            this.MessageRecaiverId = recaiverMessage;
            this.obs = "OK";
            return this;
        }
    }
}
