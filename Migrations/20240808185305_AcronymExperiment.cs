using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValVenisBE.Migrations
{
    /// <inheritdoc />
    public partial class AcronymExperiment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MissionStatements",
                keyColumn: "Id",
                keyValue: 1,
                column: "MissionStatementAcronym",
                value: "VAL : Valued Allies of LGBTQ+ <br /> VENIS: Vital Educational & NonJudgemental Informational Services <br />  VAL VENIS  ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MissionStatements",
                keyColumn: "Id",
                keyValue: 1,
                column: "MissionStatementAcronym",
                value: "VAL : Valued Allies of LGBTQ+. VENIS: Vital Educational & NonJudgemental Informational Services. VAL VENIS  ");
        }
    }
}
