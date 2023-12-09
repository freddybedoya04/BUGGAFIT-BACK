using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class add_column_tic_estipoEnvio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TIC_ESTIPOENVIO",
                table: "TIPOSCUENTAS",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TIC_ESTIPOENVIO",
                table: "TIPOSCUENTAS");
        }
    }
}
