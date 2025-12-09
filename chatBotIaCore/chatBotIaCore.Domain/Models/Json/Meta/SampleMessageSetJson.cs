using chatBotIaCore.Domain.Models.System;

namespace chatBotIaCore.Domain.Models.Json.Meta
{
    public class SampleMessageSetJson : MetaHelper
    {
        public class Root
        {
            public string messaging_product { get; set; } = string.Empty;
            public string recipient_type { get; set; } = string.Empty;
            public string to { get; set; } = string.Empty;
            public string type { get; set; } = string.Empty;
            public Text? text { get; set; } = null;
            public Document? document { get; set; } = null;
            public Image? image { get; set; } = null;
        }

        public class Text
        {
            public bool preview_url { get; set; }
            public string body { get; set; } = string.Empty;
        }

        public class Document
        {
            public string link { get; set; } = string.Empty;
            public string? caption { get; set; } = null;
            public string? filename { get; set; } = null;
        }

        public class Image
        {
            public string link { get; set; } = string.Empty;
            public string? caption { get; set; } = null;
        }

        public static SampleMessageSetJson.Root createSampleBodyRequest(IncomingMessage request)
        {
            if (request.FileResponse.fileExtension == "jpg") return createSampleImageMessage(request.Phone, request.FileResponse.filePath, request.FileResponse.fileName);

            if (request.FileResponse.fileExtension == "doc" || request.FileResponse.fileExtension == "docx" || request.FileResponse.fileExtension == "pdf") return createSampleDocumentMessage(request.Phone, request.FileResponse.filePath, string.Empty, request.FileResponse.fileName);

            return createSampleTextMessage(request.Phone, request.MessageInput);
        }

        private static SampleMessageSetJson.Root createSampleTextMessage(string to, string body)
        {
            return new SampleMessageSetJson.Root
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = to,
                type = "text",
                text = new Text
                {
                    preview_url = body.Contains("https") || body.Contains(".com") || body.Contains("http") ? true : false,
                    body = body
                }
            };
        }
        private static SampleMessageSetJson.Root createSampleDocumentMessage(string to, string documentLink, string? caption = null, string? filename = null)
        {
            return new SampleMessageSetJson.Root
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = to,
                type = "document",
                document = new Document
                {
                    link = documentLink,
                    caption = caption,
                    filename = filename
                }
            };
        }
        private static SampleMessageSetJson.Root createSampleImageMessage(string to, string imageLink, string? caption = null)
        {
            return new SampleMessageSetJson.Root
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = to,
                type = "image",
                image = new Image
                {
                    link = imageLink,
                    caption = caption
                }
            };
        }
    }
}