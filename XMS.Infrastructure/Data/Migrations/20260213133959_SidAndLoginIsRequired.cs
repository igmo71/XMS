using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class SidAndLoginIsRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "JobTitles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CostItems");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CostCategories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UsersAd",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "UsersAd",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UsersAd",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "UsersAd",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "JobTitles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "CostItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "CostCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Cities",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
