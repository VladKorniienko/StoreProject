using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ScreenshotsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Screenshots",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Screenshots",
                table: "Products");
        }
    }
}
