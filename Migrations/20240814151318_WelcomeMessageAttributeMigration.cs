using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValVenisBE.Migrations
{
    /// <inheritdoc />
    public partial class WelcomeMessageAttributeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WelcomeMessage",
                table: "MissionStatements",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MissionStatements",
                keyColumn: "Id",
                keyValue: 1,
                column: "WelcomeMessage",
                value: "Welcome to ValVenis.com!");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WelcomeMessage",
                table: "MissionStatements");
        }
    }
}
