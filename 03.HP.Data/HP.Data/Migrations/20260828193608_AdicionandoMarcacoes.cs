using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoMarcacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marcacoes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataPersistencia = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: false),
                    RelogioId = table.Column<int>(type: "int", nullable: true),
                    TipoMarcacao = table.Column<int>(type: "int", nullable: false),
                    NSR = table.Column<long>(type: "bigint", nullable: true),
                    DataHora = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: false),
                    CPF = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    PIS = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OrigemMarcacao = table.Column<int>(type: "int", nullable: false),
                    Justificativa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcacoes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_CPF_DataHora",
                table: "Marcacoes",
                columns: new[] { "CPF", "DataHora" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_DataHora",
                table: "Marcacoes",
                column: "DataHora");

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_NSR",
                table: "Marcacoes",
                column: "NSR");

            migrationBuilder.CreateIndex(
                name: "IX_Marcacoes_RelogioId_NSR",
                table: "Marcacoes",
                columns: new[] { "RelogioId", "NSR" },
                unique: true,
                filter: "[RelogioId] IS NOT NULL AND [NSR] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marcacoes");
        }
    }
}
