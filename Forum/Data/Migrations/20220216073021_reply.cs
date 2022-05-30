using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Data.Migrations
{
    public partial class reply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_UserReplyId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_UserReplyId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "UserReplyId",
                table: "Votes");

            migrationBuilder.CreateTable(
                name: "VoteReply",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    UpVoteReply = table.Column<bool>(nullable: true),
                    DownVoteReply = table.Column<bool>(nullable: true),
                    ReplyUserId = table.Column<string>(nullable: true),
                    RepliesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteReply_PostReplies_RepliesId",
                        column: x => x.RepliesId,
                        principalTable: "PostReplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoteReply_AspNetUsers_ReplyUserId",
                        column: x => x.ReplyUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoteReply_RepliesId",
                table: "VoteReply",
                column: "RepliesId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteReply_ReplyUserId",
                table: "VoteReply",
                column: "ReplyUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoteReply");

            migrationBuilder.AddColumn<string>(
                name: "UserReplyId",
                table: "Votes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserReplyId",
                table: "Votes",
                column: "UserReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_UserReplyId",
                table: "Votes",
                column: "UserReplyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
