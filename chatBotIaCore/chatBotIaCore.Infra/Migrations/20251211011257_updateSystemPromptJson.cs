using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatBotIaCore.Infra.Migrations
{
    /// <inheritdoc />
    public partial class updateSystemPromptJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BotConfiguration",
                keyColumn: "bot_id",
                keyValue: 1,
                column: "bot_system_prompt_json_response",
                value: "{\r\n              \"type\": \"object\",\r\n              \"properties\": {\r\n                \"textResponse\": {\r\n                  \"type\": \"string\",\r\n                  \"description\": \"The conversational reply to the user.\"\r\n                },\r\n                \"generateFile\": {\r\n                  \"type\": \"boolean\",\r\n                  \"description\": \"Only put this as true if the toolToUse generate a file or fetch a file or something raletd to this, if not put false\"\r\n                },\r\n                \"toolToUse\": {\r\n                  \"type\": \"string\",\r\n                  \"description\": \"The exact name of the tool to call. Empty string if no tool is needed or if its not avaible or dint exist to use in the content.\"\r\n                },\r\n                \"toolArguments\": {\r\n                  \"type\": \"string\",\r\n                  \"description\": \"A valid JSON string containing the arguments for the tool. Use \\\\\\\"{}\\\\\\\" if no tool is used.\"\r\n                },\r\n                \"chatType\": {\r\n                  \"type\": \"string\",\r\n                  \"enum\": [ \"1\", \"2\" ],\r\n                  \"description\": \"1 for Ongoing, 2 for Ended.\"\r\n                }\r\n              },\r\n              \"required\": [\r\n                \"textResponse\",\r\n                \"generateFile\",\r\n                \"toolToUse\",\r\n                \"toolArguments\",\r\n                \"chatType\"\r\n              ],\r\n              \"additionalProperties\": false\r\n            }");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BotConfiguration",
                keyColumn: "bot_id",
                keyValue: 1,
                column: "bot_system_prompt_json_response",
                value: "{ \"type\":\"object\", \"properties\":{ \"textResponse\":{ \"type\":\"string\", \"description\":\"The conversational reply to the user.\" }, \"generateFile\":{ \"type\":\"boolean\", \"description\":\"Only put this as true if the toolToUse generate a file or fetch a file or something raletd to this, if not put false\" }, \"toolToUse\":{ \"type\":\"string\", \"description\":\"The exact name of the tool to call. Empty string if no tool is needed or if its not avaible or dint exist to use in the content.\" }, \"toolArguments\":{ \"type\":\"string\", \"description\":\"A valid JSON string containing the arguments for the tool. Use \\\"{}\" if no tool is used.\" }, \"chatType\":{ \"type\":\"string\", \"enum\":[ \"1\", \"2\" ], \"description\":\"1 for Ongoing, 2 for Ended.\" } }, \"required\":[ \"textResponse\", \"generateFile\", \"toolToUse\", \"toolArguments\", \"chatType\" ], \"additionalProperties\":false }");
        }
    }
}
