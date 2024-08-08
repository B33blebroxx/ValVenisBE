using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValVenisBE.Migrations
{
    /// <inheritdoc />
    public partial class TypoFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SupportOrgs",
                keyColumn: "Id",
                keyValue: 2,
                column: "SupportOrgName",
                value: "LGBT National Coming Out Support Talkline");

            migrationBuilder.UpdateData(
                table: "SupportOrgs",
                keyColumn: "Id",
                keyValue: 4,
                column: "SupportOrgName",
                value: "LGBT National Senior Talkline");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SupportOrgs",
                keyColumn: "Id",
                keyValue: 2,
                column: "SupportOrgName",
                value: "LGBT National Coming Out Support Hotline");

            migrationBuilder.UpdateData(
                table: "SupportOrgs",
                keyColumn: "Id",
                keyValue: 4,
                column: "SupportOrgName",
                value: "LGBT National Senior Hotlne");
        }
    }
}
