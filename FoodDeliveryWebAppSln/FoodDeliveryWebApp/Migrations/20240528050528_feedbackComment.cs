using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebApp.Migrations
{
    public partial class feedbackComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FbComment_FbCommentCommentId",
                table: "Feedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FbComment",
                table: "FbComment");

            migrationBuilder.RenameTable(
                name: "FbComment",
                newName: "FbComments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FbComments",
                table: "FbComments",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FbComments_FbCommentCommentId",
                table: "Feedbacks",
                column: "FbCommentCommentId",
                principalTable: "FbComments",
                principalColumn: "CommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FbComments_FbCommentCommentId",
                table: "Feedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FbComments",
                table: "FbComments");

            migrationBuilder.RenameTable(
                name: "FbComments",
                newName: "FbComment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FbComment",
                table: "FbComment",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FbComment_FbCommentCommentId",
                table: "Feedbacks",
                column: "FbCommentCommentId",
                principalTable: "FbComment",
                principalColumn: "CommentId");
        }
    }
}
