using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tesseract.Repository.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Ocr");

            migrationBuilder.CreateTable(
                name: "Extraction",
                schema: "Ocr",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", maxLength: 100000, nullable: false),
                    TextOcr = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    DateLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extraction",
                schema: "Ocr");
        }
    }
}
