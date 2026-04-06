using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class CreateCashFlowCost : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CashFlowCost");
    }
}
