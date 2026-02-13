using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToOther : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Locations",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "JobTitles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "JobTitles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "JobTitles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Employees",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "CostItems",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "CostItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CostItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "CostCategories",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "CostCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CostCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Cities",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Cities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "JobTitles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "JobTitles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "JobTitles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CostItems");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CostItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CostItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CostCategories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CostCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CostCategories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cities");
        }
    }
}
