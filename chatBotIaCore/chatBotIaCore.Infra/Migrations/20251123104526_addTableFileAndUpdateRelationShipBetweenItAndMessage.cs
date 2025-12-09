using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatBotIaCore.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addTableFileAndUpdateRelationShipBetweenItAndMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mes_original_path",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    file_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file_type = table.Column<int>(type: "int", nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    file_size = table.Column<long>(type: "BIGINT", nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    file_original_path = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    file_created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__File__7TR8FDEYUAC1FF42", x => x.file_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FileId",
                table: "Messages",
                column: "FileId",
                unique: true,
                filter: "[FileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_File",
                table: "Messages",
                column: "FileId",
                principalTable: "File",
                principalColumn: "file_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_File",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FileId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Messages");

            migrationBuilder.AddColumn<string>(
                name: "mes_original_path",
                table: "Messages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
