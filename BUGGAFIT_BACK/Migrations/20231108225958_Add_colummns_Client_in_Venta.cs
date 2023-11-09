using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class Add_colummns_Client_in_Venta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CLI_DIRECCION",
                table: "VENTAS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CLI_NOMBRE",
                table: "VENTAS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CLI_TIPOCLIENTE",
                table: "VENTAS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CLI_UBICACION",
                table: "VENTAS",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CLI_DIRECCION",
                table: "VENTAS");

            migrationBuilder.DropColumn(
                name: "CLI_NOMBRE",
                table: "VENTAS");

            migrationBuilder.DropColumn(
                name: "CLI_TIPOCLIENTE",
                table: "VENTAS");

            migrationBuilder.DropColumn(
                name: "CLI_UBICACION",
                table: "VENTAS");
        }
    }
}
