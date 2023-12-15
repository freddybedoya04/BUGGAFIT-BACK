using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class add_columns_logicaregalos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "VEN_TIENE_REGALOSDEMAS",
                table: "VENTAS",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PRO_UNIDADREGALO",
                table: "PRODUCTOS",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PRO_UNIDAD_MINIMAREGALO",
                table: "PRODUCTOS",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VEN_TIENE_REGALOSDEMAS",
                table: "VENTAS");

            migrationBuilder.DropColumn(
                name: "PRO_UNIDADREGALO",
                table: "PRODUCTOS");

            migrationBuilder.DropColumn(
                name: "PRO_UNIDAD_MINIMAREGALO",
                table: "PRODUCTOS");
        }
    }
}
