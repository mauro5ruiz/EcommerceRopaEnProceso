using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionNet.Data.Migrations
{
    public partial class UpdateTablaProductosTmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleProductoTmps_Productos_ProductoId",
                table: "DetalleProductoTmps");

            migrationBuilder.DropIndex(
                name: "IX_DetalleProductoTmps_ProductoId",
                table: "DetalleProductoTmps");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "DetalleProductoTmps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "DetalleProductoTmps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleProductoTmps_ProductoId",
                table: "DetalleProductoTmps",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleProductoTmps_Productos_ProductoId",
                table: "DetalleProductoTmps",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
