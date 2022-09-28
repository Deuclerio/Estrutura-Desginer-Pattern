using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tesseract.Repository.Migrations
{
    public partial class NewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRegister",
                schema: "Ocr",
                table: "Extraction",
                type: "datetime2(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateLastUpdate",
                schema: "Ocr",
                table: "Extraction",
                type: "datetime2(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                schema: "Ocr",
                table: "Extraction",
                type: "varchar(3000)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                schema: "Ocr",
                table: "Extraction");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRegister",
                schema: "Ocr",
                table: "Extraction",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateLastUpdate",
                schema: "Ocr",
                table: "Extraction",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)");
        }
    }
}
