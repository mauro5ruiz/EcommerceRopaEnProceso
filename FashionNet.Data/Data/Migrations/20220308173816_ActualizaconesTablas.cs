using Microsoft.EntityFrameworkCore.Migrations;

namespace FashionNet.Data.Migrations
{
    public partial class ActualizaconesTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Talles_Productos_ProductoId",
                table: "Talles");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "Talles",
                newName: "CategoriaTalleId");

            migrationBuilder.RenameIndex(
                name: "IX_Talles_ProductoId",
                table: "Talles",
                newName: "IX_Talles_CategoriaTalleId");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "ColoresTalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoriaTalles",
                columns: table => new
                {
                    CategoriaTalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaTalles", x => x.CategoriaTalleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColoresTalles_ProductoId",
                table: "ColoresTalles",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColoresTalles_Productos_ProductoId",
                table: "ColoresTalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Talles_Categorias_CategoriaTalleId",
                table: "Talles",
                column: "CategoriaTalleId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColoresTalles_Productos_ProductoId",
                table: "ColoresTalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Talles_Categorias_CategoriaTalleId",
                table: "Talles");

            migrationBuilder.DropTable(
                name: "CategoriaTalles");

            migrationBuilder.DropIndex(
                name: "IX_ColoresTalles_ProductoId",
                table: "ColoresTalles");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "ColoresTalles");

            migrationBuilder.RenameColumn(
                name: "CategoriaTalleId",
                table: "Talles",
                newName: "ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Talles_CategoriaTalleId",
                table: "Talles",
                newName: "IX_Talles_ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Talles_Productos_ProductoId",
                table: "Talles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
