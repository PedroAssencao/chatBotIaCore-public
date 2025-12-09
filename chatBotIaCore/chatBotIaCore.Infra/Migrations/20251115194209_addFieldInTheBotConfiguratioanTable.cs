using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatBotIaCore.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addFieldInTheBotConfiguratioanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bot_Max_Output_Token_Count",
                table: "BotConfiguration",
                type: "int",
                unicode: false,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "bot_System_Enable",
                table: "BotConfiguration",
                type: "bit",
                unicode: false,
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "BotConfiguration",
                keyColumn: "bot_id",
                keyValue: 1,
                columns: new[] { "bot_Max_Output_Token_Count", "bot_System_Enable" },
                values: new object[] { 4096, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bot_Max_Output_Token_Count",
                table: "BotConfiguration");

            migrationBuilder.DropColumn(
                name: "bot_System_Enable",
                table: "BotConfiguration");
        }
    }
}
