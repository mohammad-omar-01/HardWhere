using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserHasListOfProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AverageRating", "DateAdded", "Description", "Height", "Length", "Name", "OnSale", "Price", "RawDescription", "RawPrice", "RawRegularPrice", "RawSalePrice", "RegularPrice", "ReviewCount", "SalePrice", "ShortDescription", "Sku", "Slug", "StockQuantity", "StockStatus", "Type", "Weight", "Width" },
                values: new object[] { 1, 3m, new DateTime(2023, 12, 9, 22, 19, 11, 574, DateTimeKind.Local).AddTicks(4575), "DescriptionForStepper1", 23m, 24m, "Stepper", true, 30m, "RawDescription For Stepper1", "40", "29", "30", 30m, 0, 30m, "ShorDescriptionFor Stepper1", "D1234", "stepper1", 3, "IN_STOCK", "VARIABLE", 120m, 30m });
        }
    }
}
