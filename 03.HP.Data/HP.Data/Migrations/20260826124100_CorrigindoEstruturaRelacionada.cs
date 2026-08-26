using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoEstruturaRelacionada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstruturasOrganizacionais_EstruturasOrganizacionais_EstruturaPaiId",
                table: "EstruturasOrganizacionais");

            migrationBuilder.RenameColumn(
                name: "EstruturaPaiId",
                table: "EstruturasOrganizacionais",
                newName: "EstruturaRelacionadaId");

            migrationBuilder.RenameIndex(
                name: "IX_EstruturasOrganizacionais_EstruturaPaiId",
                table: "EstruturasOrganizacionais",
                newName: "IX_EstruturasOrganizacionais_EstruturaRelacionadaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstruturasOrganizacionais_EstruturasOrganizacionais_EstruturaRelacionadaId",
                table: "EstruturasOrganizacionais",
                column: "EstruturaRelacionadaId",
                principalTable: "EstruturasOrganizacionais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstruturasOrganizacionais_EstruturasOrganizacionais_EstruturaRelacionadaId",
                table: "EstruturasOrganizacionais");

            migrationBuilder.RenameColumn(
                name: "EstruturaRelacionadaId",
                table: "EstruturasOrganizacionais",
                newName: "EstruturaPaiId");

            migrationBuilder.RenameIndex(
                name: "IX_EstruturasOrganizacionais_EstruturaRelacionadaId",
                table: "EstruturasOrganizacionais",
                newName: "IX_EstruturasOrganizacionais_EstruturaPaiId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstruturasOrganizacionais_EstruturasOrganizacionais_EstruturaPaiId",
                table: "EstruturasOrganizacionais",
                column: "EstruturaPaiId",
                principalTable: "EstruturasOrganizacionais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
