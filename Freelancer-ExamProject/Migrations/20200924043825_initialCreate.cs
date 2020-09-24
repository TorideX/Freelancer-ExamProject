using Microsoft.EntityFrameworkCore.Migrations;

namespace Freelancer_Exam.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperSkills_Skills_DeveloperId",
                table: "DeveloperSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperSkills_Developers_SkillId",
                table: "DeveloperSkills");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperSkills_Developers_DeveloperId",
                table: "DeveloperSkills",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "DeveloperId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperSkills_Skills_SkillId",
                table: "DeveloperSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperSkills_Developers_DeveloperId",
                table: "DeveloperSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperSkills_Skills_SkillId",
                table: "DeveloperSkills");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperSkills_Skills_DeveloperId",
                table: "DeveloperSkills",
                column: "DeveloperId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperSkills_Developers_SkillId",
                table: "DeveloperSkills",
                column: "SkillId",
                principalTable: "Developers",
                principalColumn: "DeveloperId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
