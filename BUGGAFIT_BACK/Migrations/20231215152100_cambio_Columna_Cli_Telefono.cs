using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class cambio_Columna_Cli_Telefono : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CLI_TELEFONO",
                table: "CLIENTES",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CLI_TELEFONO",
                table: "CLIENTES",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
