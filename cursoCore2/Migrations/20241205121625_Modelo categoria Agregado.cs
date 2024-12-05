using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cursoCore2API.Migrations
{
    /// <inheritdoc />
    public partial class ModelocategoriaAgregado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoriaId",
                table: "productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_productos_categoriaId",
                table: "productos",
                column: "categoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_productos_categoria_categoriaId",
                table: "productos",
                column: "categoriaId",
                principalTable: "categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productos_categoria_categoriaId",
                table: "productos");

            migrationBuilder.DropIndex(
                name: "IX_productos_categoriaId",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "categoriaId",
                table: "productos");
        }
    }
}
