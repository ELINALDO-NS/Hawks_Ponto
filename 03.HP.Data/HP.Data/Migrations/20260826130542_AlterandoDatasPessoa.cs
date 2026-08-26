using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HP.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoDatasPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
        name: "IX_Pessoas_Cpf",
        table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_Pis",
                table: "Pessoas");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DataNascimento",
                table: "Pessoas",
                type: "datetimeoffset(0)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DataNaoControlaPonto",
                table: "Pessoas",
                type: "datetimeoffset(0)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DataDemissao",
                table: "Pessoas",
                type: "datetimeoffset(0)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DataControlaPonto",
                table: "Pessoas",
                type: "datetimeoffset(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DataAdmissao",
                table: "Pessoas",
                type: "datetimeoffset(0)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
            
            migrationBuilder.CreateIndex(
        name: "IX_Pessoas_Cpf",
        table: "Pessoas",
        column: "Cpf",
        unique: true,
        filter: "[DataDemissao] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Pis",
                table: "Pessoas",
                column: "Pis",
                unique: true,
                filter: "[DataDemissao] IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Pessoas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNaoControlaPonto",
                table: "Pessoas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataDemissao",
                table: "Pessoas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataControlaPonto",
                table: "Pessoas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdmissao",
                table: "Pessoas",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)");
        }
    }
}
