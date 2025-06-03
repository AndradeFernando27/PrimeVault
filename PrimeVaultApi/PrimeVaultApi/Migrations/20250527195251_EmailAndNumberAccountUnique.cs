using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeVaultApi.Migrations
{
    /// <inheritdoc />
    public partial class EmailAndNumberAccountUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Conta_NumeroConta",
                table: "Conta",
                column: "NumeroConta",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Conta_NumeroConta",
                table: "Conta");
        }
    }
}
