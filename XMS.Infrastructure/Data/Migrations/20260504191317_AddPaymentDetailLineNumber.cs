using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentDetailLineNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostAllocations_UsersUt_ManagerId",
                table: "CostAllocations");

            migrationBuilder.DropIndex(
                name: "IX_CostAllocations_ManagerId",
                table: "CostAllocations");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "CostAllocations");

            migrationBuilder.AddColumn<int>(
                name: "PaymentDetailLineNumber",
                table: "CostAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDetailLineNumber",
                table: "CostAllocations");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "CostAllocations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostAllocations_ManagerId",
                table: "CostAllocations",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostAllocations_UsersUt_ManagerId",
                table: "CostAllocations",
                column: "ManagerId",
                principalTable: "UsersUt",
                principalColumn: "Id");
        }
    }
}
