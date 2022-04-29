using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionNet.Data.Migrations
{
    public partial class CreateTableColoresImagenesParaProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imagen",
                columns: table => new
                {
                    ImagenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen", x => x.ImagenId);
                    table.ForeignKey(
                        name: "FK_Imagen_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProductosColores",
                columns: table => new
                {
                    ProductoColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosColores", x => x.ProductoColorId);
                    table.ForeignKey(
                        name: "FK_ProductosColores_Colores_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colores",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductosColores_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Imagen_ProductoId",
                table: "Imagen",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosColores_ColorId",
                table: "ProductosColores",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosColores_ProductoId",
                table: "ProductosColores",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imagen");

            migrationBuilder.DropTable(
                name: "ProductosColores");
        }
    }
}
