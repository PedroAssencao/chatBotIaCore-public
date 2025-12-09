using chatBotIaCore.Storage.Interface;
using DocumentFormat.OpenXml.Packaging;
using System.Text;

namespace chatBotIaCore.Storage.Services
{
    public class ConvertionServices : IConvertionServices
    {
        public string ExtractTextFromPdf(byte[] fileBytes)
        {
            using var stream = new MemoryStream(fileBytes);
            using var document = UglyToad.PdfPig.PdfDocument.Open(stream);
            var textBuilder = new StringBuilder();
            foreach (var page in document.GetPages())
            {
                textBuilder.AppendLine(page.Text);
            }
            return textBuilder.ToString();
        }

        public string ExtractTextFromDocx(byte[] fileBytes)
        {
            using var stream = new MemoryStream(fileBytes);
            using var wordDoc = WordprocessingDocument.Open(stream, false);
            return wordDoc?.MainDocumentPart?.Document?.Body?.InnerText ?? "";
        }

        public string ExtractTextFromTxt(byte[] fileBytes)
        {
            return Encoding.UTF8.GetString(fileBytes);
        }
    }
}
