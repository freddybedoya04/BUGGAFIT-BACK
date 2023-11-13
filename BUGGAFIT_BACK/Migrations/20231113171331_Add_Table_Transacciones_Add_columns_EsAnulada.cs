using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_Transacciones_Add_columns_EsAnulada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "VEN_ESANULADA",
                table: "VENTAS",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TIC_DINEROTOTAL",
                table: "TIPOSCUENTAS",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GAS_ESANULADA",
                table: "GASTOS",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "COM_ESANULADA",
                table: "COMPRAS",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CAR_ESANULADA",
                table: "CARTERAS",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TRANSACCIONES",
                columns: table => new
                {
                    TRA_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIC_CUENTA = table.Column<int>(type: "int", nullable: false),
                    TRA_TIPO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TRA_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TRA_CONFIRMADA = table.Column<bool>(type: "bit", nullable: false),
                    TRA_ESTADO = table.Column<bool>(type: "bit", nullable: false),
                    TRA_FECHACONFIRMACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TRA_CODIGOENLACE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TRA_FUEANULADA = table.Column<bool>(type: "bit", nullable: false),
                    TRA_NUMEROTRANSACCIONBANCO = table.Column<int>(type: "int", nullable: true),
                    USU_CEDULA_CONFIRMADOR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TIC_CODIGO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSACCIONES", x => x.TRA_CODIGO);
                    table.ForeignKey(
                        name: "FK_TRANSACCIONES_TIPOSCUENTAS_TIC_CODIGO",
                        column: x => x.TIC_CODIGO,
                        principalTable: "TIPOSCUENTAS",
                        principalColumn: "TIC_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TRANSACCIONES_TIC_CODIGO",
                table: "TRANSACCIONES",
                column: "TIC_CODIGO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TRANSACCIONES");

            migrationBuilder.DropColumn(
                name: "VEN_ESANULADA",
                table: "VENTAS");

            migrationBuilder.DropColumn(
                name: "TIC_DINEROTOTAL",
                table: "TIPOSCUENTAS");

            migrationBuilder.DropColumn(
                name: "GAS_ESANULADA",
                table: "GASTOS");

            migrationBuilder.DropColumn(
                name: "COM_ESANULADA",
                table: "COMPRAS");

            migrationBuilder.DropColumn(
                name: "CAR_ESANULADA",
                table: "CARTERAS");
        }
    }
}
