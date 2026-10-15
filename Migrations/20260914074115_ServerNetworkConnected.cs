using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientMonitoring.Web.Migrations
{
    /// <inheritdoc />
    public partial class ServerNetworkConnected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiConnected",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ApplicationVersion",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LocalGateway",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LocalInterface",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LocalIp",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LocalNetworkConnected",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "MachineName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ServerGateway",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ServerInterface",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ServerIp",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApiConnected",
                table: "Clients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationVersion",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalGateway",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalInterface",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalIp",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LocalNetworkConnected",
                table: "Clients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MachineName",
                table: "Clients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServerGateway",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerInterface",
                table: "Clients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerIp",
                table: "Clients",
                type: "text",
                nullable: true);
        }
    }
}
