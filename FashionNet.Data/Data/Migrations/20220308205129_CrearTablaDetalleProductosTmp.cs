using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionNet.Data.Migrations
{
    public partial class CrearTablaDetalleProductosTmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetalleProductoTmps",
                columns: table => new
                {
                    DetalleProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    TalleId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleProductoTmps", x => x.DetalleProductoId);
                    table.ForeignKey(
                        name: "FK_DetalleProductoTmps_Colores_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colores",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleProductoTmps_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleProductoTmps_Talles_TalleId",
                        column: x => x.TalleId,
                        principalTable: "Talles",
                        principalColumn: "TalleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleProductoTmps_ColorId",
                table: "DetalleProductoTmps",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleProductoTmps_ProductoId",
                table: "DetalleProductoTmps",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleProductoTmps_TalleId",
                table: "DetalleProductoTmps",
                column: "TalleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleProductoTmps");
        }
    }
}
