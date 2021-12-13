using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EC2_1601226.Migrations
{
    public partial class CartChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Cart",
                newName: "Quantity");

            migrationBuilder.AlterColumn<int>(
                name: "BagId",
                table: "Cart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Cart",
                newName: "Count");

            migrationBuilder.AlterColumn<int>(
                name: "BagId",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "Cart",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Cart",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
