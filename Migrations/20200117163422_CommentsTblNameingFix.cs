using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio_Website_Core.Migrations
{
    public partial class CommentsTblNameingFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "viewId",
                table: "Comments",
                newName: "ViewId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Comments",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ViewId",
                table: "Comments",
                newName: "viewId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "userId");
        }
    }
}
