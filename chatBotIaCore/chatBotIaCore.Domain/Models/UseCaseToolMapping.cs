namespace chatBotIaCore.Domain.Models;

public partial class UseCaseToolMapping
{
    public int UcId { get; set; }

    public int ToolId { get; set; }

    public bool IsDefault { get; set; }

    public virtual BotTool Tool { get; set; } = null!;

    public virtual UseCase Uc { get; set; } = null!;
}
