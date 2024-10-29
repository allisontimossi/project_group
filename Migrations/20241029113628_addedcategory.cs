using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_group.Migrations
{
    /// <inheritdoc />
    public partial class addedcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX__products_CategoryId",
                table: "_products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK__products__categories_CategoryId",
                table: "_products",
                column: "CategoryId",
                principalTable: "_categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__products__categories_CategoryId",
                table: "_products");

            migrationBuilder.DropIndex(
                name: "IX__products_CategoryId",
                table: "_products");
        }
    }
}
