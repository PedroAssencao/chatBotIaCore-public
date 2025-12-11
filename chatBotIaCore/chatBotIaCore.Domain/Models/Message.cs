using chatBotIaCore.Domain.Models.System;
using chatBotIaCore.Domain.Types;
using System.ComponentModel.DataAnnotations.Schema;

namespace chatBotIaCore.Domain.Models;

public partial class Message
{
    public int MesId { get; set; }
    public int ChaId { get; set; }
    public string? MesExternalId { get; set; }
    public EMessageType MesRole { get; set; }
    public int? ConId { get; set; }
    public int? UseId { get; set; }
    public int? FileId { get; set; }
    public string MesContent { get; set; } = string.Empty;
    public string? MesProcessedContent { get; set; }
    public DateTime? MesCreatedAt { get; set; }
    [NotMapped]
    public byte[] fileData { get; set; }  = Array.Empty<byte>();
    public virtual Chat Cha { get; set; } = null!;
    public virtual Contact? Con { get; set; }
    public virtual FileModel? File { get; set; }
    public virtual User? Use { get; set; }

    // A message should have either a Contact or a User, but not both or neither, if it's not a system message
    public bool checkIfTheMessageHasContactAndUser()
    {
        if (!(this.MesRole == EMessageType.assistant))
        {
            if (Con == null && Use == null)
            {
                return false;
            }

            if (Con != null && Use != null)
            {
                return false;
            }
        }

        return true;
    }
    public static Message requestModelToMessage(IncomingMessage Model)
    {
        return new Message
        {
            MesExternalId = Model.MessageId,
            MesRole = Model.MessageType,
            MesContent = Model.MessageInput ?? "",
            MesCreatedAt = Model.createdAt,
            ChaId = Model?.Chat?.ChaId > 0 ? Model.Chat.ChaId : 0,
            ConId = Model?.Contact?.ConId == 0 || Model?.Contact?.ConId == null ? null : Model.Contact.ConId
        };
    }
    public static Message requestModelToMessageTool(IncomingMessage Model, string content)
    {
        return new Message
        {
            MesRole = EMessageType.tool,
            MesContent = content,
            MesCreatedAt = DateTime.Now,
            ChaId = Model?.Chat?.ChaId > 0 ? Model.Chat.ChaId : 0,
            ConId = Model?.Contact?.ConId == 0 || Model?.Contact?.ConId == null ? null : Model.Contact.ConId
        };
    }
    public string ReturnMessageToRequest()
    {
        if (MesProcessedContent != null && MesProcessedContent.Trim().Length > 0)
        {
            return MesProcessedContent;
        }

        return MesContent;
    }
}
