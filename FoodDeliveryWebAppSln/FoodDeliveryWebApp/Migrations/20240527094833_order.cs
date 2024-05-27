using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDeliveryWebApp.Migrations
{
    public partial class order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OId = table.Column<int>(type: "int", nullable: false),
                    FId = table.Column<int>(type: "int", nullable: false),
                    Qty_ordered = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    MenuFId = table.Column<int>(type: "int", nullable: true),
                    OrderOId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailsId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Menus_MenuFId",
                        column: x => x.MenuFId,
                        principalTable: "Menus",
                        principalColumn: "FId");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderOId",
                        column: x => x.OrderOId,
                        principalTable: "Orders",
                        principalColumn: "OId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_MenuFId",
                table: "OrderDetails",
                column: "MenuFId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderOId",
                table: "OrderDetails",
                column: "OrderOId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
