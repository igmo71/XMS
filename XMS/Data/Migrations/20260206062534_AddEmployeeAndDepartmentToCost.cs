using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeAndDepartmentToCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "CostCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "CostCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostCategories_DepartmentId",
                table: "CostCategories",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCategories_EmployeeId",
                table: "CostCategories",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCategories_Departments_DepartmentId",
                table: "CostCategories",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CostCategories_Employees_EmployeeId",
                table: "CostCategories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCategories_Departments_DepartmentId",
                table: "CostCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CostCategories_Employees_EmployeeId",
                table: "CostCategories");

            migrationBuilder.DropIndex(
                name: "IX_CostCategories_DepartmentId",
                table: "CostCategories");

            migrationBuilder.DropIndex(
                name: "IX_CostCategories_EmployeeId",
                table: "CostCategories");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CostCategories");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "CostCategories");
        }
    }
}
