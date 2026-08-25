using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoHorarioPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HorarioPessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: true),
                    HorarioId = table.Column<int>(type: "int", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioPessoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorarioPessoa_Horarios_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "Horarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HorarioPessoa_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horario_Pessoa_CargoAtualUnico",
                table: "HorarioPessoa",
                column: "PessoaId",
                unique: true,
                filter: "[DataFim] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Horario_Pessoa_Vigencia",
                table: "HorarioPessoa",
                columns: new[] { "PessoaId", "DataInicio", "DataFim" });

            migrationBuilder.CreateIndex(
                name: "IX_HorarioPessoa_HorarioId",
                table: "HorarioPessoa",
                column: "HorarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorarioPessoa");
        }
    }
}
