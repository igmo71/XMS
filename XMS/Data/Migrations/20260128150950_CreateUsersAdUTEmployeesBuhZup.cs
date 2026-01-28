using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Migrations
{
    /// <inheritdoc />
    public partial class CreateUsersAdUTEmployeesBuhZup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeBuhId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeZupId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAdId",
                table: "Employees",
                type: "nvarchar(45)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserUtId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeesBuh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletionMark = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Archived = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesBuh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesZup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletionMark = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Archived = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesZup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersAd",
                columns: table => new
                {
                    Sid = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Department = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Manager = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    DistinguishedName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAd", x => x.Sid);
                });

            migrationBuilder.CreateTable(
                name: "UsersUt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletionMark = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersUt", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeBuhId",
                table: "Employees",
                column: "EmployeeBuhId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeZupId",
                table: "Employees",
                column: "EmployeeZupId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserAdId",
                table: "Employees",
                column: "UserAdId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserUtId",
                table: "Employees",
                column: "UserUtId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeesBuh_EmployeeBuhId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeesZup_EmployeeZupId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_UsersAd_UserAdId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_UsersUt_UserUtId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeesBuh");

            migrationBuilder.DropTable(
                name: "EmployeesZup");

            migrationBuilder.DropTable(
                name: "UsersAd");

            migrationBuilder.DropTable(
                name: "UsersUt");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeBuhId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeZupId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserAdId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserUtId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeBuhId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeZupId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserAdId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserUtId",
                table: "Employees");
        }
    }
}
