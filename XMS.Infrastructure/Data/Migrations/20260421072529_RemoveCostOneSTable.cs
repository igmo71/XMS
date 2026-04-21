using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCostOneSTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostCatalog_ДДС");

            migrationBuilder.DropColumn(
                name: "Catalog_СтатьяДДС_Key",
                table: "CostAllocations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Catalog_СтатьяДДС_Key",
                table: "CostAllocations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CostCatalog_ДДС",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostCategoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Catalog_СтатьяДДС_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCatalog_ДДС", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCatalog_ДДС_CostCategoryItems_CostCategoryItemId",
                        column: x => x.CostCategoryItemId,
                        principalTable: "CostCategoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCatalog_ДДС_CostCategoryItemId",
                table: "CostCatalog_ДДС",
                column: "CostCategoryItemId");
        }
    }
}
