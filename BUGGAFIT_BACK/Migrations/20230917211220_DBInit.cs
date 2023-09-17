using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class DBInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIAS",
                columns: table => new
                {
                    CAT_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CAT_NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAT_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CAT_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIAS", x => x.CAT_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTES",
                columns: table => new
                {
                    CLI_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CLI_NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CLI_TIPOCLIENTE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CLI_UBICACION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CLI_DIRECCION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CLI_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CLI_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTES", x => x.CLI_ID);
                });

            migrationBuilder.CreateTable(
                name: "MARCAS",
                columns: table => new
                {
                    MAR_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MAR_NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MAR_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MAR_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MARCAS", x => x.MAR_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "MOTIVOSGASTOS",
                columns: table => new
                {
                    MOG_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MOG_NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MOG_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MOG_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOTIVOSGASTOS", x => x.MOG_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "TIPOSCUENTAS",
                columns: table => new
                {
                    TIC_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIC_NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIC_NUMEROREFERENCIA = table.Column<int>(type: "int", nullable: false),
                    TIC_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TIC_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPOSCUENTAS", x => x.TIC_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    USU_CEDULA = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USU_NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USU_CONTRASEÑA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USU_ROL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USU_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USU_FECHAACTUALIZACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USU_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.USU_CEDULA);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTOS",
                columns: table => new
                {
                    PRO_CODIGO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PRO_NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MAR_CODIGO = table.Column<int>(type: "int", nullable: false),
                    CAT_CODIGO = table.Column<int>(type: "int", nullable: false),
                    PRO_PRECIO_COMPRA = table.Column<float>(type: "real", nullable: false),
                    PRO_PRECIOVENTA_DETAL = table.Column<float>(type: "real", nullable: false),
                    PRO_PRECIOVENTA_MAYORISTA = table.Column<float>(type: "real", nullable: false),
                    PRO_UNIDADES_DISPONIBLES = table.Column<int>(type: "int", nullable: false),
                    PRO_UNIDADES_MINIMAS_ALERTA = table.Column<int>(type: "int", nullable: false),
                    PRO_ACTUALIZACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PRO_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PRO_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTOS", x => x.PRO_CODIGO);
                    table.ForeignKey(
                        name: "FK_PRODUCTOS_CATEGORIAS_CAT_CODIGO",
                        column: x => x.CAT_CODIGO,
                        principalTable: "CATEGORIAS",
                        principalColumn: "CAT_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCTOS_MARCAS_MAR_CODIGO",
                        column: x => x.MAR_CODIGO,
                        principalTable: "MARCAS",
                        principalColumn: "MAR_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COMPRAS",
                columns: table => new
                {
                    COM_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COM_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    COM_FECHACOMPRA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    COM_VALORCOMPRA = table.Column<float>(type: "real", nullable: false),
                    COM_PROVEEDOR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIC_CODIGO = table.Column<int>(type: "int", nullable: false),
                    COM_FECHAACTUALIZACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    COM_ENBODEGA = table.Column<bool>(type: "bit", nullable: false),
                    COM_ESTADO = table.Column<bool>(type: "bit", nullable: false),
                    COM_CREDITO = table.Column<bool>(type: "bit", nullable: false),
                    USU_CEDULA = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPRAS", x => x.COM_CODIGO);
                    table.ForeignKey(
                        name: "FK_COMPRAS_TIPOSCUENTAS_TIC_CODIGO",
                        column: x => x.TIC_CODIGO,
                        principalTable: "TIPOSCUENTAS",
                        principalColumn: "TIC_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COMPRAS_USUARIOS_USU_CEDULA",
                        column: x => x.USU_CEDULA,
                        principalTable: "USUARIOS",
                        principalColumn: "USU_CEDULA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VENTAS",
                columns: table => new
                {
                    VEN_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VEN_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VEN_FECHAVENTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VEN_TIPOPAGO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIC_CODIGO = table.Column<int>(type: "int", nullable: false),
                    CLI_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VEN_PRECIOTOTAL = table.Column<float>(type: "real", nullable: false),
                    VEN_ESTADOCREDITO = table.Column<bool>(type: "bit", nullable: true),
                    VEN_ENVIO = table.Column<bool>(type: "bit", nullable: true),
                    VEN_DOMICILIO = table.Column<bool>(type: "bit", nullable: true),
                    VEN_OBSERVACIONES = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VEN_ACTUALIZACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    USU_CEDULA = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VEN_ESTADOVENTA = table.Column<bool>(type: "bit", nullable: false),
                    VEN_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VENTAS", x => x.VEN_CODIGO);
                    table.ForeignKey(
                        name: "FK_VENTAS_CLIENTES_CLI_ID",
                        column: x => x.CLI_ID,
                        principalTable: "CLIENTES",
                        principalColumn: "CLI_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VENTAS_TIPOSCUENTAS_TIC_CODIGO",
                        column: x => x.TIC_CODIGO,
                        principalTable: "TIPOSCUENTAS",
                        principalColumn: "TIC_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VENTAS_USUARIOS_USU_CEDULA",
                        column: x => x.USU_CEDULA,
                        principalTable: "USUARIOS",
                        principalColumn: "USU_CEDULA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DETALLECOMPRAS",
                columns: table => new
                {
                    DEC_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COM_CODIGO = table.Column<int>(type: "int", nullable: false),
                    PRO_CODIGO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DEC_UNIDADES = table.Column<int>(type: "int", nullable: false),
                    DEC_PRECIOCOMPRA_PRODUCTO = table.Column<float>(type: "real", nullable: false),
                    DEC_PRECIOTOTAL = table.Column<float>(type: "real", nullable: false),
                    DEC_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DEC_FECHAACTUALIZACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DEC_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DETALLECOMPRAS", x => x.DEC_CODIGO);
                    table.ForeignKey(
                        name: "FK_DETALLECOMPRAS_COMPRAS_COM_CODIGO",
                        column: x => x.COM_CODIGO,
                        principalTable: "COMPRAS",
                        principalColumn: "COM_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DETALLECOMPRAS_PRODUCTOS_PRO_CODIGO",
                        column: x => x.PRO_CODIGO,
                        principalTable: "PRODUCTOS",
                        principalColumn: "PRO_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CARTERAS",
                columns: table => new
                {
                    CAR_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CAR_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CAR_FECHACREDITO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CAR_FECHAACTUALIZACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CAR_MOTIVO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VEN_CODIGO = table.Column<int>(type: "int", nullable: false),
                    CAR_VALORCREDITO = table.Column<float>(type: "real", nullable: false),
                    CAR_VALORABONADO = table.Column<float>(type: "real", nullable: false),
                    CAR_ESTADOCREDITO = table.Column<int>(type: "int", nullable: false),
                    CAR_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARTERAS", x => x.CAR_CODIGO);
                    table.ForeignKey(
                        name: "FK_CARTERAS_VENTAS_VEN_CODIGO",
                        column: x => x.VEN_CODIGO,
                        principalTable: "VENTAS",
                        principalColumn: "VEN_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DETALLEVENTAS",
                columns: table => new
                {
                    VED_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VEN_CODIGO = table.Column<int>(type: "int", nullable: false),
                    PRO_CODIGO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VED_UNIDADES = table.Column<int>(type: "int", nullable: false),
                    VED_PRECIOVENTA_UND = table.Column<float>(type: "real", nullable: false),
                    VED_VALORDESCUENTO_UND = table.Column<float>(type: "real", nullable: false),
                    VED_PRECIOVENTA_TOTAL = table.Column<float>(type: "real", nullable: false),
                    VED_ACTUALIZACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VED_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DETALLEVENTAS", x => x.VED_CODIGO);
                    table.ForeignKey(
                        name: "FK_DETALLEVENTAS_PRODUCTOS_PRO_CODIGO",
                        column: x => x.PRO_CODIGO,
                        principalTable: "PRODUCTOS",
                        principalColumn: "PRO_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DETALLEVENTAS_VENTAS_VEN_CODIGO",
                        column: x => x.VEN_CODIGO,
                        principalTable: "VENTAS",
                        principalColumn: "VEN_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GASTOS",
                columns: table => new
                {
                    GAS_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GAS_FECHACREACION = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GAS_FECHAGASTO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MOG_CODIGO = table.Column<int>(type: "int", nullable: false),
                    GAS_VALOR = table.Column<float>(type: "real", nullable: false),
                    TIC_CODIGO = table.Column<int>(type: "int", nullable: false),
                    GAS_ESTADO = table.Column<bool>(type: "bit", nullable: false),
                    USU_CEDULA = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GAS_PENDIENTE = table.Column<bool>(type: "bit", nullable: false),
                    VEN_CODIGO = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GASTOS", x => x.GAS_CODIGO);
                    table.ForeignKey(
                        name: "FK_GASTOS_MOTIVOSGASTOS_MOG_CODIGO",
                        column: x => x.MOG_CODIGO,
                        principalTable: "MOTIVOSGASTOS",
                        principalColumn: "MOG_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GASTOS_TIPOSCUENTAS_TIC_CODIGO",
                        column: x => x.TIC_CODIGO,
                        principalTable: "TIPOSCUENTAS",
                        principalColumn: "TIC_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GASTOS_USUARIOS_USU_CEDULA",
                        column: x => x.USU_CEDULA,
                        principalTable: "USUARIOS",
                        principalColumn: "USU_CEDULA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GASTOS_VENTAS_VEN_CODIGO",
                        column: x => x.VEN_CODIGO,
                        principalTable: "VENTAS",
                        principalColumn: "VEN_CODIGO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARTERAS_VEN_CODIGO",
                table: "CARTERAS",
                column: "VEN_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_COMPRAS_TIC_CODIGO",
                table: "COMPRAS",
                column: "TIC_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_COMPRAS_USU_CEDULA",
                table: "COMPRAS",
                column: "USU_CEDULA");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLECOMPRAS_COM_CODIGO",
                table: "DETALLECOMPRAS",
                column: "COM_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLECOMPRAS_PRO_CODIGO",
                table: "DETALLECOMPRAS",
                column: "PRO_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLEVENTAS_PRO_CODIGO",
                table: "DETALLEVENTAS",
                column: "PRO_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_DETALLEVENTAS_VEN_CODIGO",
                table: "DETALLEVENTAS",
                column: "VEN_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_GASTOS_MOG_CODIGO",
                table: "GASTOS",
                column: "MOG_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_GASTOS_TIC_CODIGO",
                table: "GASTOS",
                column: "TIC_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_GASTOS_USU_CEDULA",
                table: "GASTOS",
                column: "USU_CEDULA");

            migrationBuilder.CreateIndex(
                name: "IX_GASTOS_VEN_CODIGO",
                table: "GASTOS",
                column: "VEN_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTOS_CAT_CODIGO",
                table: "PRODUCTOS",
                column: "CAT_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTOS_MAR_CODIGO",
                table: "PRODUCTOS",
                column: "MAR_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_VENTAS_CLI_ID",
                table: "VENTAS",
                column: "CLI_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VENTAS_TIC_CODIGO",
                table: "VENTAS",
                column: "TIC_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_VENTAS_USU_CEDULA",
                table: "VENTAS",
                column: "USU_CEDULA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CARTERAS");

            migrationBuilder.DropTable(
                name: "DETALLECOMPRAS");

            migrationBuilder.DropTable(
                name: "DETALLEVENTAS");

            migrationBuilder.DropTable(
                name: "GASTOS");

            migrationBuilder.DropTable(
                name: "COMPRAS");

            migrationBuilder.DropTable(
                name: "PRODUCTOS");

            migrationBuilder.DropTable(
                name: "MOTIVOSGASTOS");

            migrationBuilder.DropTable(
                name: "VENTAS");

            migrationBuilder.DropTable(
                name: "CATEGORIAS");

            migrationBuilder.DropTable(
                name: "MARCAS");

            migrationBuilder.DropTable(
                name: "CLIENTES");

            migrationBuilder.DropTable(
                name: "TIPOSCUENTAS");

            migrationBuilder.DropTable(
                name: "USUARIOS");
        }
    }
}
