using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace chatBotIaCore.Domain.Models;

public partial class BotConfiguration
{
    private bool? _botForceJsonResponse;
    public int BotId { get; set; }
    public string BotName { get; set; } = null!;
    public string BotModelName { get; set; } = null!;
    public string BotSystemPrompt { get; set; } = null!;
    public bool BotSystemEnabled { get; set; } = false;
    public decimal BotTemperature { get; set; }
    public int BotMaxOutputTokenCount { get; set; }
    public DateTime? BotCreatedAt { get; set; }
    public DateTime? BotUpdatedAt { get; set; }
    public virtual ICollection<UseCase> UseCases { get; set; } = new List<UseCase>();
    public string BotSystemPromptJsonResponse { get; set; } = string.Empty;
    //public bool BotForceJsonResponse => BotSystemPromptJsonResponse.Length > 0;
    [NotMapped]
    public bool BotForceJsonResponse
    {
        get
        {
            return _botForceJsonResponse ?? (BotSystemPromptJsonResponse.Length > 0);
        }
        set
        {
            _botForceJsonResponse = value;
        }
    }
    public static void checkIfAllBotsAreDeactived(BotConfiguration? model)
    {
        if (model == null)
        {
            throw new ArgumentNullException("All bots are deactivated");
        }
    }

    public void returnToolsAndToolsDefinition()
    {
        List<object> toolsJsonList = new List<object>();

        foreach (var item in this.UseCases)
        {
            toolsJsonList.Add(item.UseCaseToolMappings.Select(t => new
            {
                tool_name = t.Tool.ToolName,
                description = t.Tool.ToolDescription,
                parameters = t.Tool.ToolParameters.Select(p => new
                {
                    name = p.ParamName,
                    type = p.ParamType,
                    description = p.ParamDescription,
                    required = p.ParamRequired
                })
            }));
        }

        string toolsDefinitions = JsonSerializer.Serialize(toolsJsonList, new JsonSerializerOptions { WriteIndented = true });
        this.BotSystemPrompt = this.BotSystemPrompt.Replace("{{TOOLS_DEFINITIONS}}", toolsDefinitions);
    }

    public BotConfiguration returnBotConfigurationToChatSumarry()
    {
        this.BotSystemPrompt = @"**""ATTENTION: Your SOLE AND EXCLUSIVE FUNCTION is to generate the conversation summary. You MUST NOT, under any circumstances, reply to the last turn of the conversation or greet the user. Ignore the last message and focus on analyzing the complete history. Create a concise highlight-style summary that includes: the main topics discussed, the key questions or issues raised, the important answers or solutions provided, and any decisions, conclusions, or follow-ups mentioned. DO NOT rewrite the conversation. Focus only on the essential points.""** FollowALLTHEROLESTHATAREINTHISPROMPT";
        this.BotForceJsonResponse = false;
        return this;
    }
}
