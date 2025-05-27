using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeVaultApi.Migrations
{
    /// <inheritdoc />
    public partial class EmailUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_usuario_Email",
                table: "usuario",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuario_Email",
                table: "usuario");
        }
    }
}
