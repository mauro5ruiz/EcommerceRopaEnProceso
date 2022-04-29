using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionNet.Data.Migrations
{
    public partial class CreateTableColoresTalleParaProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductosColores");

            migrationBuilder.CreateTable(
                name: "ProductoColor",
                columns: table => new
                {
                    ProductoColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoColor", x => x.ProductoColorId);
                    table.ForeignKey(
                        name: "FK_ProductoColor_Colores_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colores",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductoColor_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Talles",
                columns: table => new
                {
                    TalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talles", x => x.TalleId);
                    table.ForeignKey(
                        name: "FK_Talles_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ColoresTalles",
                columns: table => new
                {
                    ColorTalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    TalleId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColoresTalles", x => x.ColorTalleId);
                    table.ForeignKey(
                        name: "FK_ColoresTalles_Colores_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colores",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ColoresTalles_Talles_TalleId",
                        column: x => x.TalleId,
                        principalTable: "Talles",
                        principalColumn: "TalleId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColoresTalles_ColorId",
                table: "ColoresTalles",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColoresTalles_TalleId",
                table: "ColoresTalles",
                column: "TalleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoColor_ColorId",
                table: "ProductoColor",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoColor_ProductoId",
                table: "ProductoColor",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Talles_ProductoId",
                table: "Talles",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColoresTalles");

            migrationBuilder.DropTable(
                name: "ProductoColor");

            migrationBuilder.DropTable(
                name: "Talles");

            migrationBuilder.CreateTable(
                name: "ProductosColores",
                columns: table => new
                {
                    ProductoColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_ProductosColores_ColorId",
                table: "ProductosColores",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosColores_ProductoId",
                table: "ProductosColores",
                column: "ProductoId");
        }
    }
}
