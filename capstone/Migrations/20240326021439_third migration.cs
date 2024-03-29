using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone.Migrations
{
    /// <inheritdoc />
    public partial class thirdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Accountants",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Accountants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Accountants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Accountants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Accountants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Accountants");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Accountants");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Accountants");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Accountants");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Accountants",
                newName: "UserId");
        }
    }
}
