using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jogo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCategoriaPai",
                table: "Categoria",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_IdCategoriaPai",
                table: "Categoria",
                column: "IdCategoriaPai");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoria_CategoriaPai",
                table: "Categoria",
                column: "IdCategoriaPai",
                principalTable: "Categoria",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoria_CategoriaPai",
                table: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_IdCategoriaPai",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "IdCategoriaPai",
                table: "Categoria");
        }
    }
}
