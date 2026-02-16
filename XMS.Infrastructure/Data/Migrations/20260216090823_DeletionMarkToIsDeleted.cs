using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class DeletionMarkToIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeletionMark",
                table: "UsersUt",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "DeletionMark",
                table: "EmployeesZup",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "DeletionMark",
                table: "EmployeesBuh",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "UsersUt",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "EmployeesZup",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "EmployeesBuh",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CashFlowItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFolder",
                table: "CashFlowItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UsersUt");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "EmployeesZup");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "EmployeesBuh");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "CashFlowItems");

            migrationBuilder.DropColumn(
                name: "IsFolder",
                table: "CashFlowItems");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "UsersUt",
                newName: "DeletionMark");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "EmployeesZup",
                newName: "DeletionMark");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "EmployeesBuh",
                newName: "DeletionMark");
        }
    }
}
