using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoJornada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jornadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    Entrada1 = table.Column<TimeOnly>(type: "time", nullable: false),
                    Saida1 = table.Column<TimeOnly>(type: "time", nullable: false),
                    Entrada2 = table.Column<TimeOnly>(type: "time", nullable: true),
                    Saida2 = table.Column<TimeOnly>(type: "time", nullable: true),
                    Entrada3 = table.Column<TimeOnly>(type: "time", nullable: true),
                    Saida3 = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jornadas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jornadas");
        }
    }
}
