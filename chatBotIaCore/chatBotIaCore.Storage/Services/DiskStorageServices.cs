using chatBotIaCore.Domain.Models;
using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Storage.Interface;

namespace chatBotIaCore.Storage.Services
{
    public class DiskStorageServices : IStorageServices
    {
        public EStorageServiceType Type => EStorageServiceType.DiskStorage;
        protected readonly string _localHostname = string.Empty;
        public DiskStorageServices(string hostName)
        {
            _localHostname = hostName;
        }

        public async Task<FileModel> saveFileAsync(IncomingMessage request)
        {
            var filePath = FileModel.returnNewFilePath(request.File.fileName, request.File.fileExtension);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await fileStream.WriteAsync(request.File.fileData, 0, request.File.fileData.Length);
            }

            return FileModel.mappeRequestToFileModel(request, filePath, _localHostname);
        }

        public async Task<byte[]> GetFileLocal(string filePath)
        {
            try
            {
                var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                if (!File.Exists(absolutePath))
                {
                    throw new FileNotFoundException("file not found", absolutePath);
                }

                return await File.ReadAllBytesAsync(absolutePath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
