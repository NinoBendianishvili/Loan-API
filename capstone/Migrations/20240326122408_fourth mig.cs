using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone.Migrations
{
    /// <inheritdoc />
    public partial class fourthmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Accountants",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Accountants",
                newName: "password");
        }
    }
}
