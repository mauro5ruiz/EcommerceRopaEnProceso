using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionNet.Data.Migrations
{
    public partial class ErrorCategoriaTalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Talles_Categorias_CategoriaTalleId",
                table: "Talles");

            migrationBuilder.AddForeignKey(
                name: "FK_Talles_CategoriaTalles_CategoriaTalleId",
                table: "Talles",
                column: "CategoriaTalleId",
                principalTable: "CategoriaTalles",
                principalColumn: "CategoriaTalleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Talles_CategoriaTalles_CategoriaTalleId",
                table: "Talles");

            migrationBuilder.AddForeignKey(
                name: "FK_Talles_Categorias_CategoriaTalleId",
                table: "Talles",
                column: "CategoriaTalleId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
