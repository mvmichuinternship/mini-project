using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebApp.Migrations
{
    public partial class feedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FbComment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FbId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FbComment", x => x.CommentId);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FbId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    FbCommentCommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FbId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_FbComment_FbCommentCommentId",
                        column: x => x.FbCommentCommentId,
                        principalTable: "FbComment",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_Feedbacks_Menus_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Menus",
                        principalColumn: "FId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CommentId",
                table: "Feedbacks",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FbCommentCommentId",
                table: "Feedbacks",
                column: "FbCommentCommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "FbComment");
        }
    }
}
