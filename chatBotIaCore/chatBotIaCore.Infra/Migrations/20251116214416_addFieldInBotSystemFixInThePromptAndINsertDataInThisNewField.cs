using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatBotIaCore.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addFieldInBotSystemFixInThePromptAndINsertDataInThisNewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bot_system_prompt_json_response",
                table: "BotConfiguration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "BotConfiguration",
                keyColumn: "bot_id",
                keyValue: 1,
                columns: new[] { "bot_system_prompt", "bot_system_prompt_json_response" },
                values: new object[] { "You are a helpful and friendly AI conversational assistant. Your sole purpose is to process the current conversation context and respond EXCLUSIVELY with a valid JSON object. Follow these strict rules:\r\n\r\n1. Language:\r\n   - Your default response language is Portuguese-BR.\r\n   - If the user explicitly switches the language (e.g., to English or Spanish), you must reply in that language.\r\n\r\n2. Output Format:\r\n   - Your entire output must be a single JSON object that matches the LlmResponse schema defined by the system.\r\n   - Do not include any text, markdown blocks, code fences, or explanations outside the JSON object.\r\n\r\n3. Field Rules:\r\n   - textResponse: A helpful, concise conversational reply in the correct language. Markdown formatting is allowed inside the string.\r\n   - toolToUse: Must ALWAYS be an empty string (\"\").\r\n   - chatType:\r\n       - Use \"1\" for Ongoing.\r\n       - Use \"2\" for Ended.\r\n       - Only return \"2\" (Ended) when the conversation is clearly finished AND you have already confirmed with the user. Never end a conversation if there is no previous assistantMessage in the history.\r\n\r\nAlways return exactly one JSON object and nothing else.\r\n", "{\r\n                    \"type\": \"object\",\r\n                    \"properties\": {\r\n                        \"textResponse\": { \"type\": \"string\" },\r\n                        \"toolToUse\": { \"type\": \"string\" },\r\n                        \"chatType\": { \"type\": \"string\", \"enum\": [\"1\", \"2\"] }\r\n                    },\r\n                    \"required\": [\"textResponse\", \"toolToUse\", \"chatType\"],\r\n                    \"additionalProperties\": false\r\n                }" });

            migrationBuilder.UpdateData(
                table: "Contact",
                keyColumn: "con_id",
                keyValue: 1,
                columns: new[] { "con_external_id", "con_phone_number" },
                values: new object[] { "557988132044", "557988132044" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bot_system_prompt_json_response",
                table: "BotConfiguration");

            migrationBuilder.UpdateData(
                table: "BotConfiguration",
                keyColumn: "bot_id",
                keyValue: 1,
                column: "bot_system_prompt",
                value: "You are a helpful and friendly AI assistant. That will folow the roles and orderms define to delivery to the user the smoothes usege");

            migrationBuilder.UpdateData(
                table: "Contact",
                keyColumn: "con_id",
                keyValue: 1,
                columns: new[] { "con_external_id", "con_phone_number" },
                values: new object[] { "whatsapp-5579988132044", "5579988132044" });
        }
    }
}
