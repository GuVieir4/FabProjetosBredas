using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiraApi2.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMiggration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoProduto_Carrinhos_CarrinhosId",
                table: "CarrinhoProduto");

            migrationBuilder.RenameColumn(
                name: "CarrinhosId",
                table: "CarrinhoProduto",
                newName: "CarrinhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoProduto_Carrinhos_CarrinhoId",
                table: "CarrinhoProduto",
                column: "CarrinhoId",
                principalTable: "Carrinhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoProduto_Carrinhos_CarrinhoId",
                table: "CarrinhoProduto");

            migrationBuilder.RenameColumn(
                name: "CarrinhoId",
                table: "CarrinhoProduto",
                newName: "CarrinhosId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoProduto_Carrinhos_CarrinhosId",
                table: "CarrinhoProduto",
                column: "CarrinhosId",
                principalTable: "Carrinhos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
