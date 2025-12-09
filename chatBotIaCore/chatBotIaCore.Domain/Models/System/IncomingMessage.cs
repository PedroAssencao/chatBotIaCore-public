using chatBotIaCore.Domain.Types;

namespace chatBotIaCore.Domain.Models.System
{
    public class IncomingMessage
    {
        public string MessageId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public DateTime createdAt { get; set; }
        public string MessageInput { get; set; } = string.Empty;
        public EChannelType ChannelType { get; set; }
        public EMessageType MessageType { get; set; }
        public Contact Contact { get; set; } = new Contact();
        public Chat Chat { get; set; } = new Chat();
        public fileModelDto File { get; set; } = new fileModelDto();
        public fileModelDto FileResponse { get; set; } = new fileModelDto();
        public EContactType MappeChannelTypeToContact()
        {
            if (ChannelType == EChannelType.WhatsApp)
            {
                return EContactType.WhatsApp;
            }

            if (ChannelType == EChannelType.Telegram)
            {
                return EContactType.Telegram;
            }

            if (ChannelType == EChannelType.WebApplication)
            {
                return EContactType.WebApplication;
            }

            return EContactType.WhatsApp;
        }
        public IncomingMessage MappeRequestClientToRequestServer(string contect)
        {
            if (!(this.FileResponse is null))
            {
                contect = this.FileResponse.fileName;
            }

            return new IncomingMessage
            {
                MessageInput = contect,
                Chat = this.Chat,
                Phone = this.Phone,
                MessageType = EMessageType.assistant,
                ChannelType = this.ChannelType,
                createdAt = DateTime.Now,
                FileResponse = this.FileResponse!
            };
        }
        public bool checkifContactIsValid()
        {
            if (Contact == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(Contact.ConId.ToString("")))
            {
                return false;
            }
            return true;
        }
        public ESystemFileType MappeFileTypeToSystemFileType()
        {
            if (File.fileType == EFileType.Image)
            {
                return ESystemFileType.jpg;
            }

            if (File.fileType == EFileType.Video)
            {
                return ESystemFileType.mp4;
            }

            if (File.fileType == EFileType.Audio)
            {
                return ESystemFileType.ogg;
            }

            return ESystemFileType.None;
        }
    }

    public class fileModelDto()
    {
        public string fileName { get; set; } = string.Empty;
        public string id { get; set; } = string.Empty;
        public string sha256 { get; set; } = string.Empty;
        public EFileType fileType { get; set; }
        public string fileExtension { get; set; } = string.Empty;
        public byte[] fileData { get; set; } = Array.Empty<byte>();
        public string filePath { get; set; } = string.Empty;
    }
}
