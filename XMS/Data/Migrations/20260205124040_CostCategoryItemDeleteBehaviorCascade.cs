using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Migrations
{
    /// <inheritdoc />
    public partial class CostCategoryItemDeleteBehaviorCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCategoryItems_CostCategories_CategoryId",
                table: "CostCategoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CostCategoryItems_CostItems_ItemId",
                table: "CostCategoryItems");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCategoryItems_CostCategories_CategoryId",
                table: "CostCategoryItems",
                column: "CategoryId",
                principalTable: "CostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostCategoryItems_CostItems_ItemId",
                table: "CostCategoryItems",
                column: "ItemId",
                principalTable: "CostItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCategoryItems_CostCategories_CategoryId",
                table: "CostCategoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CostCategoryItems_CostItems_ItemId",
                table: "CostCategoryItems");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCategoryItems_CostCategories_CategoryId",
                table: "CostCategoryItems",
                column: "CategoryId",
                principalTable: "CostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CostCategoryItems_CostItems_ItemId",
                table: "CostCategoryItems",
                column: "ItemId",
                principalTable: "CostItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
