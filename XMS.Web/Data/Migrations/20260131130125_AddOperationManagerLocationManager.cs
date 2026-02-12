using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddOperationManagerLocationManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Cities_CityId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeesBuh_EmployeeBuhId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeesZup_EmployeeZupId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_JobTitles_JobTitleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Locations_LocationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_UsersAd_UserAdId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_UsersUt_UserUtId",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationManagerId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OperationManagerId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LocationManagerId",
                table: "Employees",
                column: "LocationManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OperationManagerId",
                table: "Employees",
                column: "OperationManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Cities_CityId",
                table: "Employees",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeesBuh_EmployeeBuhId",
                table: "Employees",
                column: "EmployeeBuhId",
                principalTable: "EmployeesBuh",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeesZup_EmployeeZupId",
                table: "Employees",
                column: "EmployeeZupId",
                principalTable: "EmployeesZup",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_LocationManagerId",
                table: "Employees",
                column: "LocationManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_OperationManagerId",
                table: "Employees",
                column: "OperationManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_JobTitles_JobTitleId",
                table: "Employees",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Locations_LocationId",
                table: "Employees",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_UsersAd_UserAdId",
                table: "Employees",
                column: "UserAdId",
                principalTable: "UsersAd",
                principalColumn: "Sid",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_UsersUt_UserUtId",
                table: "Employees",
                column: "UserUtId",
                principalTable: "UsersUt",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Cities_CityId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeesBuh_EmployeeBuhId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeesZup_EmployeeZupId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_LocationManagerId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_OperationManagerId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_JobTitles_JobTitleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Locations_LocationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_UsersAd_UserAdId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_UsersUt_UserUtId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_LocationManagerId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OperationManagerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LocationManagerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OperationManagerId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Cities_CityId",
                table: "Employees",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeesBuh_EmployeeBuhId",
                table: "Employees",
                column: "EmployeeBuhId",
                principalTable: "EmployeesBuh",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeesZup_EmployeeZupId",
                table: "Employees",
                column: "EmployeeZupId",
                principalTable: "EmployeesZup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_JobTitles_JobTitleId",
                table: "Employees",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Locations_LocationId",
                table: "Employees",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_UsersAd_UserAdId",
                table: "Employees",
                column: "UserAdId",
                principalTable: "UsersAd",
                principalColumn: "Sid");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_UsersUt_UserUtId",
                table: "Employees",
                column: "UserUtId",
                principalTable: "UsersUt",
                principalColumn: "Id");
        }
    }
}
