using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;
using chatBotIaCore.Providers.DTO.IA;

namespace chatBotIaCore.Services.DTO
{
    public class OrchestrationResult
    {
        public string ResponseText { get; set; } = string.Empty;
        public bool ShouldUpdateChatStatus { get; set; } = false;
        public EChatType NewChatStatus { get; set; }
        public bool ShouldSendFile { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public ResponseActionType ActionType { get; set; } = ResponseActionType.StandardReply;

        public static OrchestrationResult AssemblyOrcherStrarionResultResponse(LlmResponse response)
        {
            OrchestrationResult Model = new OrchestrationResult { ResponseText = response.textResponse };

            if (response.chatType.HasValue)
            {
                Model.NewChatStatus = response.chatType.Value;
                Model.ShouldUpdateChatStatus = true;
            }

            return Model;
        }

        public OrchestrationResult AssemblyOrcherStrarionResultResponseWithTool(LlmResponse response, string input, IncomingMessage request)
        {
            if (response.generateFile)
            {
                this.ShouldSendFile = true;
                this.FilePath = input;
                this.ActionType = ResponseActionType.ToolGeneratedFile;
                this.FileName = Path.GetFileName(input);
                this.ResponseText = response.textResponse;
                request.FileResponse.fileName = this.FileName;
                request.FileResponse.filePath = this.FilePath;
                request.FileResponse.fileExtension = this.FilePath.Split(".").Last();
            }

            if (!response.generateFile)
            {
                this.ResponseText = response.textResponse.Length > 0 ? response.textResponse + "\n" + input : input;
                this.ActionType = ResponseActionType.StandardReply;
            }

            return this;
        }
    }
}
