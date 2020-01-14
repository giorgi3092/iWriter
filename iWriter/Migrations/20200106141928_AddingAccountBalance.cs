using Microsoft.EntityFrameworkCore.Migrations;

namespace iWriter.Migrations
{
    public partial class AddingAccountBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AccountBalance",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountBalance",
                table: "AspNetUsers");
        }
    }
}
