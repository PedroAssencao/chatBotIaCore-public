using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatBotIaCore.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotConfiguration",
                columns: table => new
                {
                    bot_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bot_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    bot_model_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    bot_system_prompt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bot_temperature = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 2m),
                    bot_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    bot_updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BotConfi__310884E0DA9B83A7", x => x.bot_id);
                });

            migrationBuilder.CreateTable(
                name: "BotTool",
                columns: table => new
                {
                    tool_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tool_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    tool_description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    tool_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    tool_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    tool_updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BotTool__28DE264FCEAE5617", x => x.tool_id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    con_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    con_external_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    con_display_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    con_type = table.Column<int>(type: "int", nullable: false),
                    con_profile_pic_path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    con_is_client = table.Column<bool>(type: "bit", nullable: false),
                    con_is_blocked = table.Column<bool>(type: "bit", nullable: false),
                    con_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contact__081B0F1AA8EA22A9", x => x.con_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    use_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    use_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    use_password_hash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    use_email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    use_phone_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    use_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__C00A8D83911314FD", x => x.use_id);
                });

            migrationBuilder.CreateTable(
                name: "UseCase",
                columns: table => new
                {
                    uc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bot_id = table.Column<int>(type: "int", nullable: false),
                    uc_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    uc_special_prompt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uc_trigger_keywords = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    uc_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    uc_updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UseCase__9A4528808973FB5D", x => x.uc_id);
                    table.ForeignKey(
                        name: "FK_UseCase_BotConfiguration",
                        column: x => x.bot_id,
                        principalTable: "BotConfiguration",
                        principalColumn: "bot_id");
                });

            migrationBuilder.CreateTable(
                name: "ToolParameter",
                columns: table => new
                {
                    param_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tool_id = table.Column<int>(type: "int", nullable: false),
                    param_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    param_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    param_description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    param_required = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    param_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ToolPara__9282B816DCE47099", x => x.param_id);
                    table.ForeignKey(
                        name: "FK_ToolParameter_BotTool",
                        column: x => x.tool_id,
                        principalTable: "BotTool",
                        principalColumn: "tool_id");
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    cha_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    con_id = table.Column<int>(type: "int", nullable: false),
                    use_id = table.Column<int>(type: "int", nullable: true),
                    cha_status = table.Column<int>(type: "int", nullable: false),
                    cha_history = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cha_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    cha_updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Chat__5AF8FDEABAC1FF42", x => x.cha_id);
                    table.ForeignKey(
                        name: "FK_Chat_Contact",
                        column: x => x.con_id,
                        principalTable: "Contact",
                        principalColumn: "con_id");
                    table.ForeignKey(
                        name: "FK_Chat_User",
                        column: x => x.use_id,
                        principalTable: "User",
                        principalColumn: "use_id");
                });

            migrationBuilder.CreateTable(
                name: "UseCaseToolMapping",
                columns: table => new
                {
                    uc_id = table.Column<int>(type: "int", nullable: false),
                    tool_id = table.Column<int>(type: "int", nullable: false),
                    is_default = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UseCaseT__78C8CAE449D63D06", x => new { x.uc_id, x.tool_id });
                    table.ForeignKey(
                        name: "FK_UCTM_BotTool",
                        column: x => x.tool_id,
                        principalTable: "BotTool",
                        principalColumn: "tool_id");
                    table.ForeignKey(
                        name: "FK_UCTM_UseCase",
                        column: x => x.uc_id,
                        principalTable: "UseCase",
                        principalColumn: "uc_id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    mes_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cha_id = table.Column<int>(type: "int", nullable: false),
                    mes_external_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    mes_role = table.Column<int>(type: "int", nullable: false),
                    con_id = table.Column<int>(type: "int", nullable: true),
                    use_id = table.Column<int>(type: "int", nullable: true),
                    mes_content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mes_processed_content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mes_original_path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    mes_created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__86A20DC3A5F6115C", x => x.mes_id);
                    table.ForeignKey(
                        name: "FK_Messages_Chat",
                        column: x => x.cha_id,
                        principalTable: "Chat",
                        principalColumn: "cha_id");
                    table.ForeignKey(
                        name: "FK_Messages_Contact",
                        column: x => x.con_id,
                        principalTable: "Contact",
                        principalColumn: "con_id");
                    table.ForeignKey(
                        name: "FK_Messages_User",
                        column: x => x.use_id,
                        principalTable: "User",
                        principalColumn: "use_id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__BotTool__07C78DB8FC64FE8A",
                table: "BotTool",
                column: "tool_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chat_con_id",
                table: "Chat",
                column: "con_id");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_use_id",
                table: "Chat",
                column: "use_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Contact__DDF265E034CC33A2",
                table: "Contact",
                column: "con_external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_cha_id",
                table: "Messages",
                column: "cha_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_con_id",
                table: "Messages",
                column: "con_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_use_id",
                table: "Messages",
                column: "use_id");

            migrationBuilder.CreateIndex(
                name: "IX_ToolParameter_tool_id",
                table: "ToolParameter",
                column: "tool_id");

            migrationBuilder.CreateIndex(
                name: "IX_UseCase_bot_id",
                table: "UseCase",
                column: "bot_id");

            migrationBuilder.CreateIndex(
                name: "UQ__UseCase__0F2EF8FDC1861977",
                table: "UseCase",
                column: "uc_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseToolMapping_tool_id",
                table: "UseCaseToolMapping",
                column: "tool_id");

            migrationBuilder.CreateIndex(
                name: "UQ__User__221F843AC00B422F",
                table: "User",
                column: "use_email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ToolParameter");

            migrationBuilder.DropTable(
                name: "UseCaseToolMapping");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "BotTool");

            migrationBuilder.DropTable(
                name: "UseCase");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BotConfiguration");
        }
    }
}
