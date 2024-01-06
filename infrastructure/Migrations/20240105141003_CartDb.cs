using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CartDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table =>
                    new
                    {
                        cartId = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        userId = table.Column<int>(type: "int", nullable: false),
                        total = table.Column<int>(type: "int", nullable: false),
                        isEmpty = table.Column<bool>(type: "bit", nullable: false),
                        carItemscount = table.Column<int>(type: "int", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.cartId);
                }
            );

            migrationBuilder.CreateTable(
                name: "CartContents",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        CartId = table.Column<int>(type: "int", nullable: false),
                        CartProductName = table.Column<string>(
                            type: "nvarchar(max)",
                            nullable: false
                        ),
                        quantity = table.Column<int>(type: "int", nullable: false),
                        price = table.Column<int>(type: "int", nullable: false),
                        ProductId = table.Column<int>(type: "int", nullable: false),
                        ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartContents_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "cartId",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CartContents_CartId",
                table: "CartContents",
                column: "CartId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "CartContents");

            migrationBuilder.DropTable(name: "Carts");
        }
    }
}
