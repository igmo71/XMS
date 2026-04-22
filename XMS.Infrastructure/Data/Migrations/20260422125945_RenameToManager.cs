using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class RenameToManager : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_CostCategories_Employees_EmployeeId",
            table: "CostCategories");

        migrationBuilder.RenameColumn(
            name: "EmployeeId",
            table: "CostCategories",
            newName: "ManagerId");

        migrationBuilder.RenameIndex(
            name: "IX_CostCategories_EmployeeId",
            table: "CostCategories",
            newName: "IX_CostCategories_ManagerId");

        migrationBuilder.AddForeignKey(
            name: "FK_CostCategories_Employees_ManagerId",
            table: "CostCategories",
            column: "ManagerId",
            principalTable: "Employees",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_CostCategories_Employees_ManagerId",
            table: "CostCategories");

        migrationBuilder.RenameColumn(
            name: "ManagerId",
            table: "CostCategories",
            newName: "EmployeeId");

        migrationBuilder.RenameIndex(
            name: "IX_CostCategories_ManagerId",
            table: "CostCategories",
            newName: "IX_CostCategories_EmployeeId");

        migrationBuilder.AddForeignKey(
            name: "FK_CostCategories_Employees_EmployeeId",
            table: "CostCategories",
            column: "EmployeeId",
            principalTable: "Employees",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }
}
