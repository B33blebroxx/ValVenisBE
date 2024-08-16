using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ValVenisBE.Migrations
{
    /// <inheritdoc />
    public partial class SupportPageAdditionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportPage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SupportPageHeader = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportPage_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SupportPage",
                columns: new[] { "Id", "SupportPageHeader", "UserId" },
                values: new object[] { 1, "Welcome to our Support Organizations page. Here, you can find a curated list of organizations that offer essential services and support. Each entry provides detailed information about the organization, including their mission, contact details, and a link to their website. Click on each organization to learn more about the vital support they offer and how you can connect with them.", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_SupportPage_UserId",
                table: "SupportPage",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportPage");
        }
    }
}
