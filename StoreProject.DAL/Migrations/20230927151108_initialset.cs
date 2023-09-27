using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initialset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductUsers_Users_UserId1",
                table: "ProductUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductUsers",
                table: "ProductUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProductUsers_UserId1",
                table: "ProductUsers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ProductUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProductUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductUsers",
                table: "ProductUsers",
                columns: new[] { "ProductId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductUsers_UserId",
                table: "ProductUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUsers_Users_UserId",
                table: "ProductUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductUsers_Users_UserId",
                table: "ProductUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductUsers",
                table: "ProductUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProductUsers_UserId",
                table: "ProductUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProductUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "ProductUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductUsers",
                table: "ProductUsers",
                columns: new[] { "ProductId", "UserId1" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductUsers_UserId1",
                table: "ProductUsers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUsers_Users_UserId1",
                table: "ProductUsers",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
