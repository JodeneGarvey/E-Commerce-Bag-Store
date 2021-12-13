using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EC2_1601226.Migrations
{
    public partial class ShoppingCartItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Bag_BagID",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "BagID",
                table: "Cart",
                newName: "BagId");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Cart",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "CartID",
                table: "Cart",
                newName: "ShoppingCartId");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "Cart",
                newName: "ShoppingCartItemsId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_BagID",
                table: "Cart",
                newName: "IX_Cart_BagId");

            migrationBuilder.AlterColumn<int>(
                name: "BagId",
                table: "Cart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Bag_BagId",
                table: "Cart",
                column: "BagId",
                principalTable: "Bag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Bag_BagId",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "BagId",
                table: "Cart",
                newName: "BagID");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "Cart",
                newName: "CartID");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Cart",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartItemsId",
                table: "Cart",
                newName: "RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_BagId",
                table: "Cart",
                newName: "IX_Cart_BagID");

            migrationBuilder.AlterColumn<int>(
                name: "BagID",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Cart",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Bag_BagID",
                table: "Cart",
                column: "BagID",
                principalTable: "Bag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
