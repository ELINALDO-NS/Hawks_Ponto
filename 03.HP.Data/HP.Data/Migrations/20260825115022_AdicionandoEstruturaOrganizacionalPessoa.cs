using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoEstruturaOrganizacionalPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_EstruturasOrganizacionais_EstruturaId",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_EstruturaId",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "EstruturaId",
                table: "Pessoas");

            migrationBuilder.CreateTable(
                name: "EstruturaOrganizacionalPessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: true),
                    EstruturaOrganizacionalId = table.Column<int>(type: "int", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstruturaOrganizacionalPessoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstruturaOrganizacionalPessoa_EstruturasOrganizacionais_EstruturaOrganizacionalId",
                        column: x => x.EstruturaOrganizacionalId,
                        principalTable: "EstruturasOrganizacionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstruturaOrganizacionalPessoa_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstruturaOrganizacional_Pessoa_EstruturaOrganizacionalAtualUnico",
                table: "EstruturaOrganizacionalPessoa",
                column: "PessoaId",
                unique: true,
                filter: "[DataFim] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EstruturaOrganizacional_Pessoa_Vigencia",
                table: "EstruturaOrganizacionalPessoa",
                columns: new[] { "PessoaId", "DataInicio", "DataFim" });

            migrationBuilder.CreateIndex(
                name: "IX_EstruturaOrganizacionalPessoa_EstruturaOrganizacionalId",
                table: "EstruturaOrganizacionalPessoa",
                column: "EstruturaOrganizacionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstruturaOrganizacionalPessoa");

            migrationBuilder.AddColumn<int>(
                name: "EstruturaId",
                table: "Pessoas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_EstruturaId",
                table: "Pessoas",
                column: "EstruturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_EstruturasOrganizacionais_EstruturaId",
                table: "Pessoas",
                column: "EstruturaId",
                principalTable: "EstruturasOrganizacionais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
