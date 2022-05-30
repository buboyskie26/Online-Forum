using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Data.Migrations
{
    public partial class vbngsz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "PostReplies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnLike",
                table: "PostReplies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Like",
                table: "PostReplies");

            migrationBuilder.DropColumn(
                name: "UnLike",
                table: "PostReplies");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "VoteReply",
                type: "int",
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
    }
}
