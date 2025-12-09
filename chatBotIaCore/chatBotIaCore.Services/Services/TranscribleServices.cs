using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Providers.Interface.IA;
using chatBotIaCore.Services.Interfaces.Base;
using chatBotIaCore.Services.Interfaces.Transcrible;
using chatBotIaCore.Storage.Interface;

namespace chatBotIaCore.Services.Services
{
    public class TranscribleServices : ITranscribleServices
    {
        protected readonly IMessageServices _messageServices;
        protected readonly IConvertionServices _convertionServices;
        protected readonly IAiProvider _AiServices;

        public TranscribleServices(IMessageServices messageServices,
            IConvertionServices convertionServices,
            IEnumerable<IAiProvider> iaProviders)
        {
            _messageServices = messageServices;
            _convertionServices = convertionServices;
            _AiServices = iaProviders.First(x => x.providerType == EProviderType.OpenAI);
        }

        public async Task transcribleDocument(List<Message> messages)
        {
            foreach (var item in messages)
            {
                if (item.File!.FileType == ESystemFileType.ogg || item.File!.FileType == ESystemFileType.mp4 )
                {
                    item.MesProcessedContent = await _AiServices.transcribleAudio(item.fileData);
                }

                if (item.File!.FileType == ESystemFileType.jpg)
                {
                    item.MesProcessedContent = await _AiServices.extractImageContext(item.fileData);
                }

                if (item.File!.FileType == ESystemFileType.text)
                {
                    item.MesProcessedContent = _convertionServices.ExtractTextFromTxt(item.fileData);
                }

                if (item.File!.FileType == ESystemFileType.pdf)
                {
                    item.MesProcessedContent = _convertionServices.ExtractTextFromPdf(item.fileData);
                }

                if (item.File!.FileType == ESystemFileType.docx)
                {
                    item.MesProcessedContent = _convertionServices.ExtractTextFromDocx(item.fileData);
                }

                //await _messageServices.updateAsync(item); //this here could be removed, and by the solid roles it should, but i'll let this here for now.
            }
        }
    }
}
