using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class AddIdToCostCategoryItem : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_CostCategoryItems",
            table: "CostCategoryItems");

        migrationBuilder.AddColumn<Guid>(
            name: "Id",
            table: "CostCategoryItems",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddPrimaryKey(
            name: "PK_CostCategoryItems",
            table: "CostCategoryItems",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_CostCategoryItems_CategoryId",
            table: "CostCategoryItems",
            column: "CategoryId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_CostCategoryItems",
            table: "CostCategoryItems");

        migrationBuilder.DropIndex(
            name: "IX_CostCategoryItems_CategoryId",
            table: "CostCategoryItems");

        migrationBuilder.DropColumn(
            name: "Id",
            table: "CostCategoryItems");

        migrationBuilder.AddPrimaryKey(
            name: "PK_CostCategoryItems",
            table: "CostCategoryItems",
            columns: new[] { "CategoryId", "ItemId" });
    }
}
