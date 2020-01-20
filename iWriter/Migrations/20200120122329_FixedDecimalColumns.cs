using Microsoft.EntityFrameworkCore.Migrations;

namespace iWriter.Migrations
{
    public partial class FixedDecimalColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "ProjectTypes",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalance",
                table: "AspNetUsers",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "ProjectTypes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalance",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");
        }
    }
}
