using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ValVenisBE.Migrations
{
    /// <inheritdoc />
    public partial class ModelRelationshipUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissionStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    MissionStatementText = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionStatements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MissionStatements",
                columns: new[] { "Id", "MissionStatementText", "UserId" },
                values: new object[] { 1, "Our mission is to create a safe, inclusive, and supportive online space for LGBTQIA+ individuals. We aim to connect those in need with essential resources, information, and communities that uplift and empower them. Our goal is to ensure that every person can access the support they need to thrive, celebrate their identity, and build a fulfilling life.", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_SupportOrgs_UserId",
                table: "SupportOrgs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_UserId",
                table: "Quotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Logos_UserId",
                table: "Logos",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AboutMes_UserId",
                table: "AboutMes",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MissionStatements_UserId",
                table: "MissionStatements",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AboutMes_Users_UserId",
                table: "AboutMes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logos_Users_UserId",
                table: "Logos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Users_UserId",
                table: "Quotes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportOrgs_Users_UserId",
                table: "SupportOrgs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutMes_Users_UserId",
                table: "AboutMes");

            migrationBuilder.DropForeignKey(
                name: "FK_Logos_Users_UserId",
                table: "Logos");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Users_UserId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportOrgs_Users_UserId",
                table: "SupportOrgs");

            migrationBuilder.DropTable(
                name: "MissionStatements");

            migrationBuilder.DropIndex(
                name: "IX_SupportOrgs_UserId",
                table: "SupportOrgs");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_UserId",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Logos_UserId",
                table: "Logos");

            migrationBuilder.DropIndex(
                name: "IX_AboutMes_UserId",
                table: "AboutMes");
        }
    }
}
