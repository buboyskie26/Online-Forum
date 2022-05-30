using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Data.Migrations
{
    public partial class vbngs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "VoteReply",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoteReply_PostId",
                table: "VoteReply",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteReply_Posts_PostId",
                table: "VoteReply",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteReply_Posts_PostId",
                table: "VoteReply");

            migrationBuilder.DropIndex(
                name: "IX_VoteReply_PostId",
                table: "VoteReply");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "VoteReply");
        }
    }
}
