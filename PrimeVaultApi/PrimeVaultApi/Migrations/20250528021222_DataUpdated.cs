using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeVaultApi.Migrations
{
    /// <inheritdoc />
    public partial class DataUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "usuario",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditadoEm",
                table: "Conta",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "EditadoEm",
                table: "Conta");
        }
    }
}
