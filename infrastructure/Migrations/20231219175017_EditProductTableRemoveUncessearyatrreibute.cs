using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditProductTableRemoveUncessearyatrreibute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Height", table: "Products");

            migrationBuilder.DropColumn(name: "Length", table: "Products");

            migrationBuilder.DropColumn(name: "RawRegularPrice", table: "Products");

            migrationBuilder.DropColumn(name: "RawSalePrice", table: "Products");

            migrationBuilder.DropColumn(name: "RegularPrice", table: "Products");

            migrationBuilder.DropColumn(name: "ReviewCount", table: "Products");

            migrationBuilder.DropColumn(name: "Weight", table: "Products");

            migrationBuilder.DropColumn(name: "Width", table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)"
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)"
            );

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true
            );

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "RawRegularPrice",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "RawSalePrice",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<decimal>(
                name: "RegularPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "ReviewCount",
                table: "Products",
                type: "int",
                nullable: true
            );

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true
            );

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true
            );
        }
    }
}
