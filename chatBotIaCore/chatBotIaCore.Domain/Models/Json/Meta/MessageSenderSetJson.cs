namespace chatBotIaCore.Domain.Models.Json.Meta
{
    public class MessageSenderSetJson
    {
        public class Contact
        {
            public string input { get; set; } = string.Empty;
            public string wa_id { get; set; } = string.Empty;
        }

        public class Message
        {
            public string id { get; set; } = string.Empty;
        }

        public class Root
        {
            public string messaging_product { get; set; } = string.Empty;
            public List<Contact> contacts { get; set; } = new List<Contact>();
            public List<Message> messages { get; set; } = new List<Message>();
        }


    }
}
