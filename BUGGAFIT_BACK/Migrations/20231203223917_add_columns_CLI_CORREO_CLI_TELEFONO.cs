using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class add_columns_CLI_CORREO_CLI_TELEFONO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PRO_REGALO",
                table: "PRODUCTOS",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CLI_CORREO",
                table: "CLIENTES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CLI_TELEFONO",
                table: "CLIENTES",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PRO_REGALO",
                table: "PRODUCTOS");

            migrationBuilder.DropColumn(
                name: "CLI_CORREO",
                table: "CLIENTES");

            migrationBuilder.DropColumn(
                name: "CLI_TELEFONO",
                table: "CLIENTES");
        }
    }
}
