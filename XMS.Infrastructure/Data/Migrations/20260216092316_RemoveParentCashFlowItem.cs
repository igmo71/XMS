using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemoveParentCashFlowItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashFlowItems_CashFlowItems_ParentId",
                table: "CashFlowItems");

            migrationBuilder.DropIndex(
                name: "IX_CashFlowItems_ParentId",
                table: "CashFlowItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CashFlowItems_ParentId",
                table: "CashFlowItems",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashFlowItems_CashFlowItems_ParentId",
                table: "CashFlowItems",
                column: "ParentId",
                principalTable: "CashFlowItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
