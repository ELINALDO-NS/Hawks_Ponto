using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaRelacionamentoEnderecoEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdEndrereco",
                table: "Empresa",
                newName: "EndrerecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_EndrerecoId",
                table: "Empresa",
                column: "EndrerecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Endereco_EndrerecoId",
                table: "Empresa",
                column: "EndrerecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Endereco_EndrerecoId",
                table: "Empresa");

            migrationBuilder.DropIndex(
                name: "IX_Empresa_EndrerecoId",
                table: "Empresa");

            migrationBuilder.RenameColumn(
                name: "EndrerecoId",
                table: "Empresa",
                newName: "IdEndrereco");
        }
    }
}
