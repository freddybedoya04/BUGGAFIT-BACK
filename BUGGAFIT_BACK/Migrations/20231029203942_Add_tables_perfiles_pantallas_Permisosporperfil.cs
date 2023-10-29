using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    /// <inheritdoc />
    public partial class Add_tables_perfiles_pantallas_Permisosporperfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PANTALLAS",
                columns: table => new
                {
                    PAN_CODIGO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PAN_NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAN_PATH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAN_ICON = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAN_TEXT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAN_TIMESPAN = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PAN_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PANTALLAS", x => x.PAN_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "PERFILES",
                columns: table => new
                {
                    PER_CODIGO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PER_NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PER_TIMESPAN = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PER_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERFILES", x => x.PER_CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "PERIMISOSPORPERFILS",
                columns: table => new
                {
                    PPP_CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PAN_CODIGO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PER_CODIGO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PPP_VER = table.Column<bool>(type: "bit", nullable: false),
                    PPP_AGREGAR = table.Column<bool>(type: "bit", nullable: false),
                    PPP_EDITAR = table.Column<bool>(type: "bit", nullable: false),
                    PPP_ELIMINAR = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERIMISOSPORPERFILS", x => x.PPP_CODIGO);
                    table.ForeignKey(
                        name: "FK_PERIMISOSPORPERFILS_PANTALLAS_PAN_CODIGO",
                        column: x => x.PAN_CODIGO,
                        principalTable: "PANTALLAS",
                        principalColumn: "PAN_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PERIMISOSPORPERFILS_PERFILES_PER_CODIGO",
                        column: x => x.PER_CODIGO,
                        principalTable: "PERFILES",
                        principalColumn: "PER_CODIGO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PERIMISOSPORPERFILS_PAN_CODIGO",
                table: "PERIMISOSPORPERFILS",
                column: "PAN_CODIGO");

            migrationBuilder.CreateIndex(
                name: "IX_PERIMISOSPORPERFILS_PER_CODIGO",
                table: "PERIMISOSPORPERFILS",
                column: "PER_CODIGO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PERIMISOSPORPERFILS");

            migrationBuilder.DropTable(
                name: "PANTALLAS");

            migrationBuilder.DropTable(
                name: "PERFILES");
        }
    }
}
