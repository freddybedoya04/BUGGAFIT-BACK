using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_TIC_CODIGO_TO_CARTERAS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TIC_CODIGO",
                table: "CARTERAS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CARTERAS_TIC_CODIGO",
                table: "CARTERAS",
                column: "TIC_CODIGO");

            migrationBuilder.AddForeignKey(
                name: "FK_CARTERAS_TIPOSCUENTAS_TIC_CODIGO",
                table: "CARTERAS",
                column: "TIC_CODIGO",
                principalTable: "TIPOSCUENTAS",
                principalColumn: "TIC_CODIGO",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CARTERAS_TIPOSCUENTAS_TIC_CODIGO",
                table: "CARTERAS");

            migrationBuilder.DropIndex(
                name: "IX_CARTERAS_TIC_CODIGO",
                table: "CARTERAS");

            migrationBuilder.DropColumn(
                name: "TIC_CODIGO",
                table: "CARTERAS");
        }
    }
}
