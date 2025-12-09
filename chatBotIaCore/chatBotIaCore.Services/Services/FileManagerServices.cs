using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Infra.Interfaces;
using chatBotIaCore.Providers.Interface;
using chatBotIaCore.Services.Interfaces.Base;
using chatBotIaCore.Services.Interfaces.Transcrible;
using chatBotIaCore.Storage.Interface;
using static System.Net.WebRequestMethods;

namespace chatBotIaCore.Services.Services
{
    public class FileManagerServices : BaseServices<FileModel>, IFileManagerServices
    {
        protected readonly IStorageServices _storageServices;
        protected readonly IMessageProcessingHandler _MetaServices;
        protected readonly IFileInterface _fileRepository;
        protected readonly ITranscribleServices _transcribleServices;
        protected readonly IMessageServices _messageServices;
        public FileManagerServices(IBaseInterface<FileModel> repository,
            IEnumerable<IStorageServices> storageServices,
            IEnumerable<IMessageProcessingHandler> MetaServicesList,
            IBaseServices<FileModel> fileServices,
            IFileInterface fileRepositry,
            ITranscribleServices transcribleServices,
            IMessageServices messageServices) : base(repository)
        {
            _storageServices = storageServices.First(x => x.Type == EStorageServiceType.DiskStorage);
            _MetaServices = MetaServicesList.First(x => x.ChannelHandler == EChannelType.WhatsApp);
            _fileRepository = fileRepositry;
            _transcribleServices = transcribleServices;
            _messageServices = messageServices;
        }

        public async Task<FileModel?> getFileAsync(string path)
        {
            FileModel? file = await _fileRepository.getFileByPath(path);

            if (file != null)
            {
                file.fileData = await _storageServices.GetFileLocal(file.FilePath);
            }

            return file;
        }

        [AI_Tool("toolTest")]
        public string toolTest(string content)
        {
            //return content + " - processed by FileManagerServices.toolTest"; //test with arguments
            return "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/330px-Placeholder_view_vector.svg.jpg"; //file just to test a function that generate and return a file.
        }

        public async Task<FileModel> processFileAsync(IncomingMessage request)
        {
            string url = await _MetaServices.getMidiaDownloadUrlAsync(request.File.id);
            request.File.fileData = await _MetaServices.getMidiaDownloadAsync(url);

            FileModel response = await _storageServices.saveFileAsync(request);
            await createAsync(response);

            Message message = request.Chat.Messages.Last();
            message.fileData = request.File.fileData;
            await _transcribleServices.transcribleDocument(new List<Message> { message });

            await _messageServices.updateAsync(message);
            return response;
        }
    }
}
