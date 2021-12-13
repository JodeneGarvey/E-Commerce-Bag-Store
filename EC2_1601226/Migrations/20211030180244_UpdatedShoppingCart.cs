using Microsoft.EntityFrameworkCore.Migrations;

namespace EC2_1601226.Migrations
{
    public partial class UpdatedShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ShoppingCartItems",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderDetails",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ShoppingCartItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderDetails",
                newName: "Quantity");
        }
    }
}
