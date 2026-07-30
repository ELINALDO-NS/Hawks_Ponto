using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alterarandonomesdetabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Endereco_EndrerecoId",
                table: "Empresa");

            migrationBuilder.DropForeignKey(
                name: "FK_EstruturaOrganizacional_Empresa_EmpresaId",
                table: "EstruturaOrganizacional");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstruturaOrganizacional",
                table: "EstruturaOrganizacional");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empresa",
                table: "Empresa");

            migrationBuilder.RenameTable(
                name: "EstruturaOrganizacional",
                newName: "EstruturasOrganizacionais");

            migrationBuilder.RenameTable(
                name: "Endereco",
                newName: "Enderecos");

            migrationBuilder.RenameTable(
                name: "Empresa",
                newName: "Empresas");

            migrationBuilder.RenameIndex(
                name: "IX_EstruturaOrganizacional_EmpresaId",
                table: "EstruturasOrganizacionais",
                newName: "IX_EstruturasOrganizacionais_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Empresa_EndrerecoId",
                table: "Empresas",
                newName: "IX_Empresas_EndrerecoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstruturasOrganizacionais",
                table: "EstruturasOrganizacionais",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empresas",
                table: "Empresas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Enderecos_EndrerecoId",
                table: "Empresas",
                column: "EndrerecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstruturasOrganizacionais_Empresas_EmpresaId",
                table: "EstruturasOrganizacionais",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Enderecos_EndrerecoId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_EstruturasOrganizacionais_Empresas_EmpresaId",
                table: "EstruturasOrganizacionais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstruturasOrganizacionais",
                table: "EstruturasOrganizacionais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empresas",
                table: "Empresas");

            migrationBuilder.RenameTable(
                name: "EstruturasOrganizacionais",
                newName: "EstruturaOrganizacional");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                newName: "Endereco");

            migrationBuilder.RenameTable(
                name: "Empresas",
                newName: "Empresa");

            migrationBuilder.RenameIndex(
                name: "IX_EstruturasOrganizacionais_EmpresaId",
                table: "EstruturaOrganizacional",
                newName: "IX_EstruturaOrganizacional_EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Empresas_EndrerecoId",
                table: "Empresa",
                newName: "IX_Empresa_EndrerecoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstruturaOrganizacional",
                table: "EstruturaOrganizacional",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereco",
                table: "Endereco",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empresa",
                table: "Empresa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Endereco_EndrerecoId",
                table: "Empresa",
                column: "EndrerecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstruturaOrganizacional_Empresa_EmpresaId",
                table: "EstruturaOrganizacional",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
