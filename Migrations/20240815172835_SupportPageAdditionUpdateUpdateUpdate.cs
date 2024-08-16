using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValVenisBE.Migrations
{
    /// <inheritdoc />
    public partial class SupportPageAdditionUpdateUpdateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupportPageIntro",
                table: "SupportPages",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SupportPages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SupportPageHeader", "SupportPageIntro" },
                values: new object[] { "Support Organizations", "Welcome to our Support Organizations page. Here, you can find a curated list of organizations that offer essential services and support. Each entry provides detailed information about the organization, including their mission, contact details, and a link to their website. Click on each organization to learn more about the vital support they offer and how you can connect with them." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportPageIntro",
                table: "SupportPages");

            migrationBuilder.UpdateData(
                table: "SupportPages",
                keyColumn: "Id",
                keyValue: 1,
                column: "SupportPageHeader",
                value: "Welcome to our Support Organizations page. Here, you can find a curated list of organizations that offer essential services and support. Each entry provides detailed information about the organization, including their mission, contact details, and a link to their website. Click on each organization to learn more about the vital support they offer and how you can connect with them.");
        }
    }
}
