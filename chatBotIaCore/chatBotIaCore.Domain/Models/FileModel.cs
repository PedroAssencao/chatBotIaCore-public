using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatBotIaCore.Domain.Models
{
    public class FileModel
    {
        public int FileId { get; set; }
        public ESystemFileType FileType { get; set; } = ESystemFileType.None;
        public string FileName { get; set; } = string.Empty;
        public Int64 FileSize { get; set; } = Int64.MinValue;
        public string FilePath { get; set; } = string.Empty;
        public string FileOriginalPath { get; set; } = string.Empty;
        public DateTime FileCreateAt { get; set; } = DateTime.MinValue;
        [NotMapped]
        public byte[] fileData { get; set; } = Array.Empty<byte>();
        public virtual Message Message { get; set; } = new Message();
        public static string returnFolderPath()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }
        public static string returnNewFileName(string fileName, string fileExtension) => $"{Path.GetFileName(fileName)}_{DateTime.Now.ToString("yyyyMMdd_HHmmssSSS")}.{fileExtension}";
        public static string returnNewFilePath(string fileName, string fileExtension, string? hostName = "") => hostName + Path.Combine(returnFolderPath(), returnNewFileName(fileName, fileExtension));
        public static FileModel mappeRequestToFileModel(IncomingMessage request, string filePath, string? hostName = "") => new FileModel
        {
            FileCreateAt = DateTime.Now,
            FileName = request.File.fileName,
            FileSize = request.File.fileData.Length,
            FileOriginalPath = filePath,
            FilePath = hostName?.Length > 0 ? hostName + filePath : filePath,
            FileType = request.MappeFileTypeToSystemFileType(),
            Message = request.Chat.Messages.Last()
        };
    }
}
