using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValVenisBE.Migrations
{
    /// <inheritdoc />
    public partial class SupportPageAdditionUpdateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportPage_Users_UserId",
                table: "SupportPage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupportPage",
                table: "SupportPage");

            migrationBuilder.RenameTable(
                name: "SupportPage",
                newName: "SupportPages");

            migrationBuilder.RenameIndex(
                name: "IX_SupportPage_UserId",
                table: "SupportPages",
                newName: "IX_SupportPages_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupportPages",
                table: "SupportPages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportPages_Users_UserId",
                table: "SupportPages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportPages_Users_UserId",
                table: "SupportPages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupportPages",
                table: "SupportPages");

            migrationBuilder.RenameTable(
                name: "SupportPages",
                newName: "SupportPage");

            migrationBuilder.RenameIndex(
                name: "IX_SupportPages_UserId",
                table: "SupportPage",
                newName: "IX_SupportPage_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupportPage",
                table: "SupportPage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportPage_Users_UserId",
                table: "SupportPage",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
