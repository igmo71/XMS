using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateSkuInventoryUt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkuInventoryUt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkuInventoryUt", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkuInventoryUt_ScuId_WarehouseId",
                table: "SkuInventoryUt",
                columns: new[] { "ScuId", "WarehouseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkuInventoryUt");
        }
    }
}
