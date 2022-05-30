using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Data.Migrations
{
    public partial class nmjx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVoteDownReply",
                table: "Votes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVoteReply",
                table: "Votes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVoteDownReply",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "IsVoteReply",
                table: "Votes");
        }
    }
}
