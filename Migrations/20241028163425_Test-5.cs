using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_group.Migrations
{
    /// <inheritdoc />
    public partial class Test5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    ClientCode = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK__purchases__customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "_customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__purchases__products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "_products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__purchases_CustomerId",
                table: "_purchases",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX__purchases_ProductId",
                table: "_purchases",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_categories");

            migrationBuilder.DropTable(
                name: "_purchases");

            migrationBuilder.DropTable(
                name: "_customers");

            migrationBuilder.DropTable(
                name: "_products");
        }
    }
}
