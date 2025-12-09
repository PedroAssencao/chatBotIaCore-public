using chatBotIaCore.Domain.Types;

namespace chatBotIaCore.Domain.Models;

public partial class Chat
{
    public int ChaId { get; set; }

    public int ConId { get; set; }

    public int? UseId { get; set; }

    public EChatType ChaStatus { get; set; }

    public string? ChaHistory { get; set; }

    public DateTime? ChaCreatedAt { get; set; }

    public DateTime? ChaUpdatedAt { get; set; }

    public virtual Contact Con { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User? Use { get; set; }
}
