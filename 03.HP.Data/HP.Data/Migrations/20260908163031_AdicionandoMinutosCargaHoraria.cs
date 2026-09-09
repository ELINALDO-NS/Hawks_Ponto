using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoMinutosCargaHoraria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinutosCargaHoraria",
                table: "Jornadas",
                type: "int",
                nullable: false,
                computedColumnSql: "DATEDIFF(MINUTE, Entrada1, Saida1) + ISNULL(DATEDIFF(MINUTE, Entrada2, Saida2), 0) + ISNULL(DATEDIFF(MINUTE, Entrada3, Saida3), 0)",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinutosCargaHoraria",
                table: "Jornadas");
        }
    }
}
