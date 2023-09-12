using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectfolio.Data.Migrations
{
    public partial class TableChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usage",
                table: "technologies");

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "technologies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "technologies");

            migrationBuilder.AddColumn<string>(
                name: "Usage",
                table: "technologies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
