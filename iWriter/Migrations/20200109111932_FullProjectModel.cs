using Microsoft.EntityFrameworkCore.Migrations;

namespace iWriter.Migrations
{
    public partial class FullProjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTypeFeatures_ProjectTypes_ProjectTypeId",
                table: "ProjectTypeFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTypes",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectTypes");

            migrationBuilder.AddColumn<int>(
                name: "ProjectTypeId",
                table: "ProjectTypes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTypes",
                table: "ProjectTypes",
                column: "ProjectTypeId");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(maxLength: 50, nullable: false),
                    GeneralTopic = table.Column<string>(maxLength: 30, nullable: false),
                    ProjectDetails = table.Column<string>(maxLength: 10000, nullable: false),
                    KeywordDensity = table.Column<float>(nullable: false),
                    ArticleQuantity = table.Column<int>(nullable: false),
                    WordCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectProjectTypes",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProjectTypes", x => new { x.ProjectId, x.ProjectTypeId });
                    table.ForeignKey(
                        name: "FK_ProjectProjectTypes_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectProjectTypes_ProjectTypes_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalTable: "ProjectTypes",
                        principalColumn: "ProjectTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProjectTypes_ProjectTypeId",
                table: "ProjectProjectTypes",
                column: "ProjectTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTypeFeatures_ProjectTypes_ProjectTypeId",
                table: "ProjectTypeFeatures",
                column: "ProjectTypeId",
                principalTable: "ProjectTypes",
                principalColumn: "ProjectTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTypeFeatures_ProjectTypes_ProjectTypeId",
                table: "ProjectTypeFeatures");

            migrationBuilder.DropTable(
                name: "ProjectProjectTypes");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTypes",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "ProjectTypeId",
                table: "ProjectTypes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjectTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTypes",
                table: "ProjectTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTypeFeatures_ProjectTypes_ProjectTypeId",
                table: "ProjectTypeFeatures",
                column: "ProjectTypeId",
                principalTable: "ProjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
