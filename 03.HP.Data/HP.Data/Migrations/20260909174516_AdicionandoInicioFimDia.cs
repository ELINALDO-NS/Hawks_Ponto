using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoInicioFimDia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "InicioFimDia",
                table: "Horarios",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AlterColumn<int>(
                name: "MinutosCargaHoraria",
                table: "Jornadas",
                type: "int",
                nullable: false,
                computedColumnSql: "(CASE WHEN DATEDIFF(MINUTE, Entrada1, Saida1) < 0 THEN DATEDIFF(MINUTE, Entrada1, Saida1) + 1440 ELSE DATEDIFF(MINUTE, Entrada1, Saida1) END) + ISNULL(CASE WHEN DATEDIFF(MINUTE, Entrada2, Saida2) < 0 THEN DATEDIFF(MINUTE, Entrada2, Saida2) + 1440 ELSE DATEDIFF(MINUTE, Entrada2, Saida2) END, 0) + ISNULL(CASE WHEN DATEDIFF(MINUTE, Entrada3, Saida3) < 0 THEN DATEDIFF(MINUTE, Entrada3, Saida3) + 1440 ELSE DATEDIFF(MINUTE, Entrada3, Saida3) END, 0)",
                stored: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "DATEDIFF(MINUTE, Entrada1, Saida1) + ISNULL(DATEDIFF(MINUTE, Entrada2, Saida2), 0) + ISNULL(DATEDIFF(MINUTE, Entrada3, Saida3), 0)",
                oldStored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InicioFimDia",
                table: "Horarios");

            migrationBuilder.AlterColumn<int>(
                name: "MinutosCargaHoraria",
                table: "Jornadas",
                type: "int",
                nullable: false,
                computedColumnSql: "DATEDIFF(MINUTE, Entrada1, Saida1) + ISNULL(DATEDIFF(MINUTE, Entrada2, Saida2), 0) + ISNULL(DATEDIFF(MINUTE, Entrada3, Saida3), 0)",
                stored: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "(CASE WHEN DATEDIFF(MINUTE, Entrada1, Saida1) < 0 THEN DATEDIFF(MINUTE, Entrada1, Saida1) + 1440 ELSE DATEDIFF(MINUTE, Entrada1, Saida1) END) + ISNULL(CASE WHEN DATEDIFF(MINUTE, Entrada2, Saida2) < 0 THEN DATEDIFF(MINUTE, Entrada2, Saida2) + 1440 ELSE DATEDIFF(MINUTE, Entrada2, Saida2) END, 0) + ISNULL(CASE WHEN DATEDIFF(MINUTE, Entrada3, Saida3) < 0 THEN DATEDIFF(MINUTE, Entrada3, Saida3) + 1440 ELSE DATEDIFF(MINUTE, Entrada3, Saida3) END, 0)",
                oldStored: true);
        }
    }
}
