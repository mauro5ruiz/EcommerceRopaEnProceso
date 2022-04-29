using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionNet.Data.Migrations
{
    public partial class EdicionCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoColor");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "DetalleProductoTmps");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "ColoresTalles");

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Productos");

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "DetalleProductoTmps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Precio",
                table: "ColoresTalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductoColor",
                columns: table => new
                {
                    ProductoColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoColor", x => x.ProductoColorId);
                    table.ForeignKey(
                        name: "FK_ProductoColor_Colores_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colores",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoColor_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoColor_ColorId",
                table: "ProductoColor",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoColor_ProductoId",
                table: "ProductoColor",
                column: "ProductoId");
        }
    }
}
