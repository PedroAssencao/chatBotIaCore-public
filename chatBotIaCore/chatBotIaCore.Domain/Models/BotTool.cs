namespace chatBotIaCore.Domain.Models;

public partial class BotTool
{
    public int ToolId { get; set; }

    public string ToolName { get; set; } = null!;

    public string ToolDescription { get; set; } = null!;

    public bool ToolActive { get; set; }

    public DateTime? ToolCreatedAt { get; set; }

    public DateTime? ToolUpdatedAt { get; set; }

    public virtual ICollection<ToolParameter> ToolParameters { get; set; } = new List<ToolParameter>();

    public virtual ICollection<UseCaseToolMapping> UseCaseToolMappings { get; set; } = new List<UseCaseToolMapping>();
}
