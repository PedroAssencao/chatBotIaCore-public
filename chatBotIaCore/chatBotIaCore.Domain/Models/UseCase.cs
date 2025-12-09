namespace chatBotIaCore.Domain.Models;

public partial class UseCase
{
    public int UcId { get; set; }

    public int BotId { get; set; }

    public string UcName { get; set; } = null!;

    public string? UcSpecialPrompt { get; set; }

    public string? UcTriggerKeywords { get; set; }

    public DateTime? UcCreatedAt { get; set; }

    public DateTime? UcUpdatedAt { get; set; }

    public virtual BotConfiguration Bot { get; set; } = null!;

    public virtual ICollection<UseCaseToolMapping> UseCaseToolMappings { get; set; } = new List<UseCaseToolMapping>();
}
