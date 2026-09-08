using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoDiaApontamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiaApontamentos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataUltAtualizacao = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: true),
                    DataApontamento = table.Column<DateOnly>(type: "date", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: true),
                    MinutosTrabalhados = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MinutosAtraso = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MinutosFalta = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MinutosExtra = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MinutosCredito = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MinutosDebito = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MinutosAdicionalNoturno = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MinutosDSR = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaApontamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaApontamentos_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiaApontamentos_DataApontamento",
                table: "DiaApontamentos",
                column: "DataApontamento");

            migrationBuilder.CreateIndex(
                name: "IX_DiaApontamentos_PessoaId",
                table: "DiaApontamentos",
                column: "PessoaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaApontamentos");
        }
    }
}
