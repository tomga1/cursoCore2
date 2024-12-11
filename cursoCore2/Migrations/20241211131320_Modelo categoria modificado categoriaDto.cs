using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cursoCore2API.Migrations
{
    /// <inheritdoc />
    public partial class ModelocategoriamodificadocategoriaDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categoria",
                newName: "categoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "categoriaId",
                table: "categoria",
                newName: "Id");
        }
    }
}
