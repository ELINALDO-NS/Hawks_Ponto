using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoHorarioIdDiasApontamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HorarioId",
                table: "DiaApontamentos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiaApontamentos_HorarioId",
                table: "DiaApontamentos",
                column: "HorarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaApontamentos_Horarios_HorarioId",
                table: "DiaApontamentos",
                column: "HorarioId",
                principalTable: "Horarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiaApontamentos_Horarios_HorarioId",
                table: "DiaApontamentos");

            migrationBuilder.DropIndex(
                name: "IX_DiaApontamentos_HorarioId",
                table: "DiaApontamentos");

            migrationBuilder.DropColumn(
                name: "HorarioId",
                table: "DiaApontamentos");
        }
    }
}
