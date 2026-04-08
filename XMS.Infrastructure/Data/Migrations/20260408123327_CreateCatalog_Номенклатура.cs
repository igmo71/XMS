using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateCatalog_Номенклатура : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashFlowCost");

            migrationBuilder.DropTable(
                name: "SkuInventoryUt");

            migrationBuilder.DropTable(
                name: "CashFlowItems");

            migrationBuilder.CreateTable(
                name: "1c_ut_Catalog_Номенклатура",
                columns: table => new
                {
                    Ref_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataVersion = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    DeletionMark = table.Column<bool>(type: "bit", nullable: false),
                    Parent_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFolder = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_1c_ut_Catalog_Номенклатура", x => x.Ref_Key);
                });

            migrationBuilder.CreateTable(
                name: "CostCatalog_ДДС",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostCategoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Catalog_СтатьиДвиженияДенежныхСредств_RefKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "1c_ut_Catalog_Номенклатура");

            migrationBuilder.DropTable(
                name: "CostCatalog_ДДС");

            migrationBuilder.CreateTable(
                name: "CashFlowItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsFolder = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlowItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkuInventoryUt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ScuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkuInventoryUt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashFlowCost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CashFlowItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostCategoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlowCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashFlowCost_CashFlowItems_CashFlowItemId",
                        column: x => x.CashFlowItemId,
                        principalTable: "CashFlowItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CashFlowCost_CostCategoryItems_CostCategoryItemId",
                        column: x => x.CostCategoryItemId,
                        principalTable: "CostCategoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashFlowCost_CashFlowItemId",
                table: "CashFlowCost",
                column: "CashFlowItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CashFlowCost_CostCategoryItemId",
                table: "CashFlowCost",
                column: "CostCategoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SkuInventoryUt_ScuId_WarehouseId",
                table: "SkuInventoryUt",
                columns: new[] { "ScuId", "WarehouseId" },
                unique: true);
        }
    }
}
