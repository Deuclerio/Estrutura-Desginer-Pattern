using Microsoft.EntityFrameworkCore.Migrations;

namespace Tesseract.Repository.Migrations
{
    public partial class Add_MeanConfidence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "id",
                schema: "Ocr",
                table: "Extraction");

            migrationBuilder.AddColumn<float>(
                name: "MeanConfidence",
                schema: "Ocr",
                table: "Extraction",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "Id",
                schema: "Ocr",
                table: "Extraction",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Id",
                schema: "Ocr",
                table: "Extraction");

            migrationBuilder.DropColumn(
                name: "MeanConfidence",
                schema: "Ocr",
                table: "Extraction");

            migrationBuilder.AddPrimaryKey(
                name: "id",
                schema: "Ocr",
                table: "Extraction",
                column: "Id");
        }
    }
}
