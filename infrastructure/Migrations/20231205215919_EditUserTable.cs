using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillingAddressID",
                table: "Users",
                type: "int",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ShippingAddressID",
                table: "Users",
                type: "int",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table =>
                    new
                    {
                        AddressID = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Country = table.Column<int>(type: "int", nullable: true),
                        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Company = table.Column<string>(type: "nvarchar(max)", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_BillingAddressID",
                table: "Users",
                column: "BillingAddressID"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShippingAddressID",
                table: "Users",
                column: "ShippingAddressID"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_BillingAddressID",
                table: "Users",
                column: "BillingAddressID",
                principalTable: "Address",
                principalColumn: "AddressID"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_ShippingAddressID",
                table: "Users",
                column: "ShippingAddressID",
                principalTable: "Address",
                principalColumn: "AddressID"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_BillingAddressID",
                table: "Users"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_ShippingAddressID",
                table: "Users"
            );

            migrationBuilder.DropTable(name: "Address");

            migrationBuilder.DropIndex(name: "IX_Users_BillingAddressID", table: "Users");

            migrationBuilder.DropIndex(name: "IX_Users_ShippingAddressID", table: "Users");

            migrationBuilder.DropColumn(name: "BillingAddressID", table: "Users");

            migrationBuilder.DropColumn(name: "FirstName", table: "Users");

            migrationBuilder.DropColumn(name: "LastName", table: "Users");

            migrationBuilder.DropColumn(name: "ShippingAddressID", table: "Users");

            migrationBuilder.DropColumn(name: "Token", table: "Users");
        }
    }
}
