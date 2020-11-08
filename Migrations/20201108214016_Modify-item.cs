using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Migrations
{
    public partial class Modifyitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photopath1",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photopath2",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photopath1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Photopath2",
                table: "Items");
        }
    }
}
