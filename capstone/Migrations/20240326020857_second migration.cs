using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_userId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Loans",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_userId",
                table: "Loans",
                newName: "IX_Loans_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Loans",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                newName: "IX_Loans_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_userId",
                table: "Loans",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
