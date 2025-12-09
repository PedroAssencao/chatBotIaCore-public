namespace chatBotIaCore.Domain.Models;

public partial class ToolParameter
{
    public int ParamId { get; set; }

    public int ToolId { get; set; }

    public string ParamName { get; set; } = null!;

    public string ParamType { get; set; } = null!;

    public string ParamDescription { get; set; } = null!;

    public bool ParamRequired { get; set; }

    public DateTime? ParamCreatedAt { get; set; }

    public virtual BotTool Tool { get; set; } = null!;
}
