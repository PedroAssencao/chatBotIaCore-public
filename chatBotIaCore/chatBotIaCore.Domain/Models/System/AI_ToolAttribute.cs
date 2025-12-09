namespace chatBotIaCore.Domain.Models.System
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class AI_ToolAttribute : Attribute
    {
        public string Name { get; }
        public AI_ToolAttribute(string name)
        {
            Name = name;
        }
    }
}
