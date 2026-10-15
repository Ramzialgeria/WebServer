using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientMonitoring.Web.Migrations.ElectionDb
{
    /// <inheritdoc />
    public partial class SyncApplicationDbModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CircoAPW",
                columns: table => new
                {
                    CodeCircoAPW = table.Column<string>(type: "text", nullable: false),
                    CodeWilaya = table.Column<string>(type: "text", nullable: false),
                    LibelleAr = table.Column<string>(type: "text", nullable: false),
                    LibelleFr = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircoAPW", x => x.CodeCircoAPW);
                });

            migrationBuilder.CreateTable(
                name: "CircoAPC",
                columns: table => new
                {
                    CodeCircoAPC = table.Column<string>(type: "text", nullable: false),
                    LIBELLEARABE = table.Column<string>(type: "text", nullable: false),
                    CodeCircoAPW = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircoAPC", x => x.CodeCircoAPC);
                    table.ForeignKey(
                        name: "FK_CircoAPC_CircoAPW_CodeCircoAPW",
                        column: x => x.CodeCircoAPW,
                        principalTable: "CircoAPW",
                        principalColumn: "CodeCircoAPW",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListeElectoral",
                columns: table => new
                {
                    IdListe = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LibelleAr = table.Column<string>(type: "text", nullable: false),
                    NumIdentifiant = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    CodeCircoAPC = table.Column<string>(type: "text", nullable: true),
                    CodeCircoAPW = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeElectoral", x => x.IdListe);
                    table.ForeignKey(
                        name: "FK_ListeElectoral_CircoAPC_CodeCircoAPC",
                        column: x => x.CodeCircoAPC,
                        principalTable: "CircoAPC",
                        principalColumn: "CodeCircoAPC",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListeElectoral_CircoAPW_CodeCircoAPW",
                        column: x => x.CodeCircoAPW,
                        principalTable: "CircoAPW",
                        principalColumn: "CodeCircoAPW",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidature",
                columns: table => new
                {
                    IdCandidat = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomAr = table.Column<string>(type: "text", nullable: false),
                    PrenomAr = table.Column<string>(type: "text", nullable: false),
                    NomFr = table.Column<string>(type: "text", nullable: false),
                    PrenomFr = table.Column<string>(type: "text", nullable: false),
                    IdListeElectoral = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidature", x => x.IdCandidat);
                    table.ForeignKey(
                        name: "FK_Candidature_ListeElectoral_IdListeElectoral",
                        column: x => x.IdListeElectoral,
                        principalTable: "ListeElectoral",
                        principalColumn: "IdListe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidature_IdListeElectoral",
                table: "Candidature",
                column: "IdListeElectoral");

            migrationBuilder.CreateIndex(
                name: "IX_CircoAPC_CodeCircoAPW",
                table: "CircoAPC",
                column: "CodeCircoAPW");

            migrationBuilder.CreateIndex(
                name: "IX_ListeElectoral_CodeCircoAPC",
                table: "ListeElectoral",
                column: "CodeCircoAPC");

            migrationBuilder.CreateIndex(
                name: "IX_ListeElectoral_CodeCircoAPW",
                table: "ListeElectoral",
                column: "CodeCircoAPW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidature");

            migrationBuilder.DropTable(
                name: "ListeElectoral");

            migrationBuilder.DropTable(
                name: "CircoAPC");

            migrationBuilder.DropTable(
                name: "CircoAPW");
        }
    }
}
