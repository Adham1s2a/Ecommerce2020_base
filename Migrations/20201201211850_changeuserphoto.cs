using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Migrations
{
    public partial class changeuserphoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPhoto",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPhoto",
                table: "AspNetUsers");
        }
    }
}
