namespace chatBotIaCore.Domain.Models;

public partial class User
{
    public int UseId { get; set; }

    public string UseName { get; set; } = null!;

    public string UsePasswordHash { get; set; } = null!;

    public string UseEmail { get; set; } = null!;

    public string? UsePhoneNumber { get; set; }

    public DateTime? UseCreatedAt { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
