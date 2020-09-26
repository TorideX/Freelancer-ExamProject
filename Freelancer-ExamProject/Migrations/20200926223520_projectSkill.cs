using Microsoft.EntityFrameworkCore.Migrations;

namespace Freelancer_Exam.Migrations
{
    public partial class projectSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Skills_RequiredSkillSkillId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_RequiredSkillSkillId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "RequiredSkillSkillId",
                table: "Projects");

            migrationBuilder.CreateTable(
                name: "ProjectSkill",
                columns: table => new
                {
                    ProjectId = table.Column<string>(nullable: false),
                    SkillId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSkill", x => new { x.ProjectId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_ProjectSkill_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSkill_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkill_SkillId",
                table: "ProjectSkill",
                column: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectSkill");

            migrationBuilder.AddColumn<string>(
                name: "RequiredSkillSkillId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_RequiredSkillSkillId",
                table: "Projects",
                column: "RequiredSkillSkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Skills_RequiredSkillSkillId",
                table: "Projects",
                column: "RequiredSkillSkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
