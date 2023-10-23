using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LPA.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Link_Competition_Phase_Rencontre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rencontre_Competition_CompetitionId",
                table: "Rencontre");

            migrationBuilder.DropIndex(
                name: "IX_Rencontre_CompetitionId",
                table: "Rencontre");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Rencontre");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                table: "Phase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ordre",
                table: "Phase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Phase_CompetitionId",
                table: "Phase",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phase_Competition_CompetitionId",
                table: "Phase",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phase_Competition_CompetitionId",
                table: "Phase");

            migrationBuilder.DropIndex(
                name: "IX_Phase_CompetitionId",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "Ordre",
                table: "Phase");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                table: "Rencontre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rencontre_CompetitionId",
                table: "Rencontre",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rencontre_Competition_CompetitionId",
                table: "Rencontre",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
