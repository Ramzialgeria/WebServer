using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientMonitoring.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemoveServerNetworkConnected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServerNetworkConnected",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ServerNetworkConnected",
                table: "Clients",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
