using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Freelancer_Exam.Migrations
{
    public partial class ConfirmedRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "BidRequests");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "BidRequests");

            migrationBuilder.AddColumn<short>(
                name: "RatingCount",
                table: "Developers",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "DaysToFinish",
                table: "BidRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "BidRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ConfirmedRequests",
                columns: table => new
                {
                    ConfirmedRequestId = table.Column<string>(nullable: false),
                    BidRequestId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmedRequests", x => x.ConfirmedRequestId);
                    table.ForeignKey(
                        name: "FK_ConfirmedRequests_BidRequests_BidRequestId",
                        column: x => x.BidRequestId,
                        principalTable: "BidRequests",
                        principalColumn: "BidRequestId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmedRequests_BidRequestId",
                table: "ConfirmedRequests",
                column: "BidRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmedRequests");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "DaysToFinish",
                table: "BidRequests");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "BidRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "BidRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "BidRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
