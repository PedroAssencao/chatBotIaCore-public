namespace chatBotIaCore.Domain.Models.Json.Meta
{
    public class MessageWithDocumentAttachedSetJson
    {
        public class Change
        {
            public Value value { get; set; } = new Value();
            public string field { get; set; } = string.Empty;
        }

        public class Contact
        {
            public Profile profile { get; set; } = new Profile();
            public string wa_id { get; set; } = string.Empty;
        }

        public class Document
        {
            public string caption { get; set; } = string.Empty;
            public string filename { get; set; } = string.Empty;
            public string mime_type { get; set; } = string.Empty;
            public string sha256 { get; set; } = string.Empty;
            public string id { get; set; } = string.Empty;  
        }

        public class Entry
        {
            public string id { get; set; } = string.Empty;
            public List<Change> changes { get; set; } = new List<Change>(); 
        }

        public class Message
        {
            public string from { get; set; } = string.Empty;
            public string id { get; set; } = string.Empty;
            public string timestamp { get; set; } = string.Empty;
            public string type { get; set; } = string.Empty;
            public Document document { get; set; } = new Document();
        }

        public class Metadata
        {
            public string display_phone_number { get; set; } = string.Empty;
            public string phone_number_id { get; set; } = string.Empty;
        }

        public class Profile
        {
            public string name { get; set; } = string.Empty;
        }

        public class Root
        {
            public string @object { get; set; } = string.Empty;
            public List<Entry> entry { get; set; } = new List<Entry>();
        }

        public class Value
        {
            public string messaging_product { get; set; } = string.Empty;
            public Metadata metadata { get; set; } = new Metadata();
            public List<Contact> contacts { get; set; } = new List<Contact>();
            public List<Message> messages { get; set; } = new List<Message>();
        }
    }
}
