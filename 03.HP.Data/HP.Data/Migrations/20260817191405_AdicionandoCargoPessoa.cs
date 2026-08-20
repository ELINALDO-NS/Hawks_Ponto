using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCargoPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Cargos_CargoId",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_CargoId",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "Pessoas");

            migrationBuilder.CreateTable(
                name: "CargoPessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: false),
                    DataFim = table.Column<DateTime>(type: "date", nullable: true),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoPessoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoPessoa_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CargoPessoa_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoPessoa_CargoId",
                table: "CargoPessoa",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoPessoa_Pessoa_CargoAtualUnico",
                table: "CargoPessoa",
                column: "PessoaId",
                unique: true,
                filter: "[DataFim] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CargoPessoa_Pessoa_Vigencia",
                table: "CargoPessoa",
                columns: new[] { "PessoaId", "DataInicio", "DataFim" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoPessoa");

            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "Pessoas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_CargoId",
                table: "Pessoas",
                column: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Cargos_CargoId",
                table: "Pessoas",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
