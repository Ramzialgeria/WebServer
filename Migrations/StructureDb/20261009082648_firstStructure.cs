using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientMonitoring.Web.Migrations.StructureDb
{
    /// <inheritdoc />
    public partial class firstStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BureauV",
                columns: table => new
                {
                    ID_Bureau = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CBureau = table.Column<string>(type: "text", nullable: false),
                    Nb_Inscrits = table.Column<int>(type: "integer", nullable: true),
                    ID_center = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BureauV", x => x.ID_Bureau);
                });

            migrationBuilder.CreateTable(
                name: "Center_V",
                columns: table => new
                {
                    ID_center = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descriptio_ar = table.Column<string>(type: "text", nullable: false),
                    adress = table.Column<string>(type: "text", nullable: false),
                    id_structure = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Center_V", x => x.ID_center);
                });

            migrationBuilder.CreateTable(
                name: "Wilaya",
                columns: table => new
                {
                    codeWilaya = table.Column<string>(type: "text", nullable: false),
                    Libele = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wilaya", x => x.codeWilaya);
                });

            migrationBuilder.CreateTable(
                name: "Commune",
                columns: table => new
                {
                    codecommune = table.Column<string>(type: "text", nullable: false),
                    libeleCommune = table.Column<string>(type: "text", nullable: false),
                    codewilaya = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commune", x => x.codecommune);
                    table.ForeignKey(
                        name: "FK_Commune_Wilaya_codewilaya",
                        column: x => x.codewilaya,
                        principalTable: "Wilaya",
                        principalColumn: "codeWilaya",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commune_codewilaya",
                table: "Commune",
                column: "codewilaya");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BureauV");

            migrationBuilder.DropTable(
                name: "Center_V");

            migrationBuilder.DropTable(
                name: "Commune");

            migrationBuilder.DropTable(
                name: "Wilaya");
        }
    }
}
