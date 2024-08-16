using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValVenisBE.Migrations
{
    /// <inheritdoc />
    public partial class AboutMeAdditionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMeHeader",
                table: "AboutMes",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AboutMes",
                keyColumn: "Id",
                keyValue: 1,
                column: "AboutMeHeader",
                value: "About Me");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMeHeader",
                table: "AboutMes");
        }
    }
}
