using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class Add_table_TIPOSENVIOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TIP_CODIGO",
                table: "VENTAS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TIPOSENVIOS",
                columns: table => new
                {
                    TIP_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIP_NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIP_VALOR = table.Column<float>(type: "real", nullable: false),
                    TIP_TIMESPAN = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TIP_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPOSENVIOS", x => x.TIP_CODIGO);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VENTAS_TIP_CODIGO",
                table: "VENTAS",
                column: "TIP_CODIGO");

            migrationBuilder.AddForeignKey(
                name: "FK_VENTAS_TIPOSENVIOS_TIP_CODIGO",
                table: "VENTAS",
                column: "TIP_CODIGO",
                principalTable: "TIPOSENVIOS",
                principalColumn: "TIP_CODIGO",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VENTAS_TIPOSENVIOS_TIP_CODIGO",
                table: "VENTAS");

            migrationBuilder.DropTable(
                name: "TIPOSENVIOS");

            migrationBuilder.DropIndex(
                name: "IX_VENTAS_TIP_CODIGO",
                table: "VENTAS");

            migrationBuilder.DropColumn(
                name: "TIP_CODIGO",
                table: "VENTAS");
        }
    }
}
