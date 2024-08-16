using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ValVenisBE.Migrations
{
    /// <inheritdoc />
    public partial class QuotePageAddMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuotePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    QuotePageHeader = table.Column<string>(type: "text", nullable: true),
                    QuotePageIntro = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotePages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotePages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "QuotePages",
                columns: new[] { "Id", "QuotePageHeader", "QuotePageIntro", "UserId" },
                values: new object[] { 1, "Quotes", "Here are some quotes that inspire us", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_QuotePages_UserId",
                table: "QuotePages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotePages");
        }
    }
}
