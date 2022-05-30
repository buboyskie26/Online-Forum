using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Data.Migrations
{
    public partial class nmj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserReplyId",
                table: "Votes",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
