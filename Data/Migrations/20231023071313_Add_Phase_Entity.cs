using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LPA.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Phase_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhaseId",
                table: "Rencontre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Phase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phase", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rencontre_PhaseId",
                table: "Rencontre",
                column: "PhaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rencontre_Phase_PhaseId",
                table: "Rencontre",
                column: "PhaseId",
                principalTable: "Phase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rencontre_Phase_PhaseId",
                table: "Rencontre");

            migrationBuilder.DropTable(
                name: "Phase");

            migrationBuilder.DropIndex(
                name: "IX_Rencontre_PhaseId",
                table: "Rencontre");

            migrationBuilder.DropColumn(
                name: "PhaseId",
                table: "Rencontre");
        }
    }
}
