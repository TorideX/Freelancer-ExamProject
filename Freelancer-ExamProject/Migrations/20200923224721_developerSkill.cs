using Microsoft.EntityFrameworkCore.Migrations;

namespace Freelancer_Exam.Migrations
{
    public partial class developerSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Developers_DeveloperId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_DeveloperId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeveloperSkills",
                columns: table => new
                {
                    DeveloperId = table.Column<string>(nullable: false),
                    SkillId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperSkills", x => new { x.DeveloperId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_DeveloperSkills_Skills_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeveloperSkills_Developers_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Developers",
                        principalColumn: "DeveloperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperSkills_SkillId",
                table: "DeveloperSkills",
                column: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperSkills");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "DeveloperId",
                table: "Skills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_DeveloperId",
                table: "Skills",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Developers_DeveloperId",
                table: "Skills",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "DeveloperId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
