using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutodijeloviDemic.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageMimeType",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageMimeType",
                table: "Categories");
        }
    }
}
