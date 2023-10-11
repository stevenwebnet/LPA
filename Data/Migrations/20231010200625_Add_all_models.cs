using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LPA.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_all_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTournoi_ApplicationUser_UsersId",
                table: "ApplicationUserTournoi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserTournoi",
                table: "ApplicationUserTournoi");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserTournoi_UsersId",
                table: "ApplicationUserTournoi");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ApplicationUserTournoi",
                newName: "ApplicationUsersId");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionId",
                table: "Tournoi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RencontreId",
                table: "Pronostics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserTournoi",
                table: "ApplicationUserTournoi",
                columns: new[] { "ApplicationUsersId", "TournoisId" });

            migrationBuilder.CreateTable(
                name: "Equipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lieu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lieu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LieuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competition_Lieu_LieuId",
                        column: x => x.LieuId,
                        principalTable: "Lieu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionEquipe",
                columns: table => new
                {
                    CompetitionsId = table.Column<int>(type: "int", nullable: false),
                    EquipesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionEquipe", x => new { x.CompetitionsId, x.EquipesId });
                    table.ForeignKey(
                        name: "FK_CompetitionEquipe_Competition_CompetitionsId",
                        column: x => x.CompetitionsId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionEquipe_Equipe_EquipesId",
                        column: x => x.EquipesId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rencontre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScroreEquipeDomicile = table.Column<int>(type: "int", nullable: true),
                    ScroreEquipeExterieur = table.Column<int>(type: "int", nullable: true),
                    EquipeDomicileId = table.Column<int>(type: "int", nullable: false),
                    EquipeExterieurId = table.Column<int>(type: "int", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rencontre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rencontre_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rencontre_Equipe_EquipeDomicileId",
                        column: x => x.EquipeDomicileId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rencontre_Equipe_EquipeExterieurId",
                        column: x => x.EquipeExterieurId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournoi_CompetitionId",
                table: "Tournoi",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pronostics_RencontreId",
                table: "Pronostics",
                column: "RencontreId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTournoi_TournoisId",
                table: "ApplicationUserTournoi",
                column: "TournoisId");

            migrationBuilder.CreateIndex(
                name: "IX_Competition_LieuId",
                table: "Competition",
                column: "LieuId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEquipe_EquipesId",
                table: "CompetitionEquipe",
                column: "EquipesId");

            migrationBuilder.CreateIndex(
                name: "IX_Rencontre_CompetitionId",
                table: "Rencontre",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rencontre_EquipeDomicileId",
                table: "Rencontre",
                column: "EquipeDomicileId");

            migrationBuilder.CreateIndex(
                name: "IX_Rencontre_EquipeExterieurId",
                table: "Rencontre",
                column: "EquipeExterieurId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTournoi_ApplicationUser_ApplicationUsersId",
                table: "ApplicationUserTournoi",
                column: "ApplicationUsersId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pronostics_Rencontre_RencontreId",
                table: "Pronostics",
                column: "RencontreId",
                principalTable: "Rencontre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournoi_Competition_CompetitionId",
                table: "Tournoi",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserTournoi_ApplicationUser_ApplicationUsersId",
                table: "ApplicationUserTournoi");

            migrationBuilder.DropForeignKey(
                name: "FK_Pronostics_Rencontre_RencontreId",
                table: "Pronostics");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournoi_Competition_CompetitionId",
                table: "Tournoi");

            migrationBuilder.DropTable(
                name: "CompetitionEquipe");

            migrationBuilder.DropTable(
                name: "Rencontre");

            migrationBuilder.DropTable(
                name: "Competition");

            migrationBuilder.DropTable(
                name: "Equipe");

            migrationBuilder.DropTable(
                name: "Lieu");

            migrationBuilder.DropIndex(
                name: "IX_Tournoi_CompetitionId",
                table: "Tournoi");

            migrationBuilder.DropIndex(
                name: "IX_Pronostics_RencontreId",
                table: "Pronostics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserTournoi",
                table: "ApplicationUserTournoi");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserTournoi_TournoisId",
                table: "ApplicationUserTournoi");

            migrationBuilder.DropColumn(
                name: "CompetitionId",
                table: "Tournoi");

            migrationBuilder.DropColumn(
                name: "RencontreId",
                table: "Pronostics");

            migrationBuilder.RenameColumn(
                name: "ApplicationUsersId",
                table: "ApplicationUserTournoi",
                newName: "UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserTournoi",
                table: "ApplicationUserTournoi",
                columns: new[] { "TournoisId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTournoi_UsersId",
                table: "ApplicationUserTournoi",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserTournoi_ApplicationUser_UsersId",
                table: "ApplicationUserTournoi",
                column: "UsersId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
