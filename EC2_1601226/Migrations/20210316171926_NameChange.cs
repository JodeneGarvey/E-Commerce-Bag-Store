using Microsoft.EntityFrameworkCore.Migrations;

namespace EC2_1601226.Migrations
{
    public partial class NameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Bag_BagId",
                table: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "ShoppingCartItems");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_BagId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_BagId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Order",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems",
                column: "ShoppingCartItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Bag_BagId",
                table: "ShoppingCartItems",
                column: "BagId",
                principalTable: "Bag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Bag_BagId",
                table: "ShoppingCartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems");

            migrationBuilder.RenameTable(
                name: "ShoppingCartItems",
                newName: "Cart");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_BagId",
                table: "Cart",
                newName: "IX_Cart_BagId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "ShoppingCartItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Bag_BagId",
                table: "Cart",
                column: "BagId",
                principalTable: "Bag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
