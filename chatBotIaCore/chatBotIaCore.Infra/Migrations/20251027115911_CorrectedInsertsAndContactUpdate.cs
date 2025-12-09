using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatBotIaCore.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedInsertsAndContactUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "con_phone_number",
                table: "Contact",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "BotConfiguration",
                columns: new[] { "bot_id", "bot_created_at", "bot_model_name", "bot_name", "bot_system_prompt", "bot_temperature", "bot_updated_at",  },
                values: new object[] { 1, new DateTime(2025, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc), "openai/gpt-4o-mini", "Default Assistant", "You are a helpful and friendly AI conversational assistant. Your sole purpose is to process the current conversation context and respond **EXCLUSIVELY** with a valid JSON object. You MUST follow these strict rules:\n\n1. **Language:** Your default response language is **Portuguese-BR**. If the user explicitly switches the language (e.g., to English or Spanish), you must adapt and reply in that language.\n\n2. **Output Format (Strict Schema):** Your entire output MUST be a single JSON object matching the `LlmResponse` schema below. Do not include any text, markdown, or explanation outside of this JSON.\n\n```json\n{\n  \"textResponse\": \"[The conversational reply in the required language]\",\n  \"toolToUse\": \"\",\n  \"chatType\": \"[Ongoing = 1, or Ended = 2 (you must put the number in the place not the legend, ex if you id put Ongoing, insetead youll put = 1 and continue like this.)]\"\n}\n```\n\n3. **Field Rules:**\n   * **`textResponse`**: Provide a helpful, concise, and well-formatted conversational reply. Use markdown within the string (e.g., **bolding**).\n   * **`toolToUse`**: For the current development phase, this field MUST be an **empty string** (`\"\"`).\n   * **`chatType`**: Choose one of the following exact string values based on the conversation context:\n     * \n     * `Ongoing`: Use when the user is actively pursuing a complex task or request that requires follow-up, information gathering, or decision-making.\n     * `Ended`: Use when the user or you think that he dont want to chat anymore or 2 when you see that the problem was solved (rember to Always ask first, if you dont have a assitantMessage before in the chat dont end the conversation)", 0.7m, new DateTime(2025, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "BotTool",
                columns: new[] { "tool_id", "tool_active", "tool_created_at", "tool_description", "tool_name", "tool_updated_at" },
                values: new object[] { 1, true, new DateTime(2025, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc), "Tool to search for information from the internet.", "google_search", new DateTime(2025, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "con_id", "con_created_at", "con_display_name", "con_external_id", "con_is_blocked", "con_is_client", "con_phone_number", "con_profile_pic_path", "con_type" },
                values: new object[] { 1, new DateTime(2025, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc), "Pedro Assenção", "557988132044", false, true, "557988132044", null, 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "use_id", "use_created_at", "use_email", "use_name", "use_password_hash", "use_phone_number" },
                values: new object[] { 1, new DateTime(2025, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc), "pedro31@gmail.com", "PedroAssencao", "pedro.123", "5579988132044" });

            migrationBuilder.InsertData(
                table: "ToolParameter",
                columns: new[] { "param_id", "param_created_at", "param_description", "param_name", "param_required", "param_type", "tool_id" },
                values: new object[] { 1, new DateTime(2025, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc), "One or multiple queries to Google Search.", "queries", true, "ARRAY", 1 });

            migrationBuilder.InsertData(
                table: "UseCase",
                columns: new[] { "uc_id", "bot_id", "uc_created_at", "uc_name", "uc_special_prompt", "uc_trigger_keywords", "uc_updated_at" },
                values: new object[] { 1, 1, new DateTime(2025, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc), "General Summary and Help", "You are engaging in a general conversation with a customer. Be polite and concise.", "", new DateTime(2025, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "UseCaseToolMapping",
                columns: new[] { "tool_id", "uc_id", "is_default" },
                values: new object[] { 1, 1, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contact",
                keyColumn: "con_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToolParameter",
                keyColumn: "param_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UseCaseToolMapping",
                keyColumns: new[] { "tool_id", "uc_id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "use_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BotTool",
                keyColumn: "tool_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UseCase",
                keyColumn: "uc_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BotConfiguration",
                keyColumn: "bot_id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "con_phone_number",
                table: "Contact");
        }
    }
}
