using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_into_Detalleven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PRO_PRECIO_COMPRA",
                table: "DETALLEVENTAS",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PRO_PRECIO_COMPRA",
                table: "DETALLEVENTAS");
        }
    }
}
