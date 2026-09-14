using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoStatusCalculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "StatusCalculo",
                table: "DiaApontamentos",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_DiaApontamento_Pessoa_Data",
                table: "DiaApontamentos",
                columns: new[] { "PessoaId", "DataApontamento" })
                .Annotation("SqlServer:Include", new[] { "MinutosTrabalhados", "MinutosExtra", "MinutosAtraso", "StatusCalculo" });

            migrationBuilder.CreateIndex(
                name: "IX_DiaApontamento_StatusCalculo",
                table: "DiaApontamentos",
                column: "StatusCalculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DiaApontamento_Pessoa_Data",
                table: "DiaApontamentos");

            migrationBuilder.DropIndex(
                name: "IX_DiaApontamento_StatusCalculo",
                table: "DiaApontamentos");

            migrationBuilder.DropColumn(
                name: "StatusCalculo",
                table: "DiaApontamentos");
        }
    }
}
