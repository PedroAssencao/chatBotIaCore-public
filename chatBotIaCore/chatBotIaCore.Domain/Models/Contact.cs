using chatBotIaCore.Domain.Types;
using System.Text;

namespace chatBotIaCore.Domain.Models;

public partial class Contact
{
    public int ConId { get; set; }
    public string ConExternalId { get; set; } = null!;
    public string ConPhoneNumber { get; set; } = null!;
    public string? ConDisplayName { get; set; }
    public EContactType ConType { get; set; }
    public string? ConProfilePicPath { get; set; }
    public bool ConIsClient { get; set; }
    public bool ConIsBlocked { get; set; }
    public DateTime? ConCreatedAt { get; set; }
    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    public string? normalizeName() => ConDisplayName?.Normalize(NormalizationForm.FormC);
    public string contactValidate()
    {
        if (this.ConIsBlocked)
        {
            return "Cliente bloqueado";
        }

        if (!this.ConIsClient)
        {
            return "Não e cliente do sistema";
        }

        return "";
    }
}
