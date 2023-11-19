using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class Add_column_usu_cedula_to_carteras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "USU_CEDULA",
                table: "CARTERAS",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "USU_CEDULA",
                table: "CARTERAS");
        }
    }
}
