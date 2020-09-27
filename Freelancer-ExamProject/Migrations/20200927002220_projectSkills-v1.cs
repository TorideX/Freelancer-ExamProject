using Microsoft.EntityFrameworkCore.Migrations;

namespace Freelancer_Exam.Migrations
{
    public partial class projectSkillsv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSkill_Projects_ProjectId",
                table: "ProjectSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSkill_Skills_SkillId",
                table: "ProjectSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSkill",
                table: "ProjectSkill");

            migrationBuilder.RenameTable(
                name: "ProjectSkill",
                newName: "ProjectSkills");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectSkill_SkillId",
                table: "ProjectSkills",
                newName: "IX_ProjectSkills_SkillId");

            migrationBuilder.AddColumn<string>(
                name: "ProjectSkillId",
                table: "ProjectSkills",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSkills",
                table: "ProjectSkills",
                columns: new[] { "ProjectId", "SkillId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSkills_Projects_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSkills_Skills_SkillId",
                table: "ProjectSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSkills_Projects_ProjectId",
                table: "ProjectSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSkills_Skills_SkillId",
                table: "ProjectSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectSkills",
                table: "ProjectSkills");

            migrationBuilder.DropColumn(
                name: "ProjectSkillId",
                table: "ProjectSkills");

            migrationBuilder.RenameTable(
                name: "ProjectSkills",
                newName: "ProjectSkill");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectSkills_SkillId",
                table: "ProjectSkill",
                newName: "IX_ProjectSkill_SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectSkill",
                table: "ProjectSkill",
                columns: new[] { "ProjectId", "SkillId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSkill_Projects_ProjectId",
                table: "ProjectSkill",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSkill_Skills_SkillId",
                table: "ProjectSkill",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
