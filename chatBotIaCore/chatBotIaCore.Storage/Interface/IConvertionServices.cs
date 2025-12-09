namespace chatBotIaCore.Storage.Interface
{
    public interface IConvertionServices
    {
        string ExtractTextFromPdf(byte[] fileBytes);
        string ExtractTextFromDocx(byte[] fileBytes);
        string ExtractTextFromTxt(byte[] fileBytes);
    }
}
