using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class AddDataVersion : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "DataVersion",
            table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
            type: "nvarchar(16)",
            maxLength: 16,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "DataVersion",
            table: "1c_ut_Catalog_СтатьиДвиженияДенежныхСредств",
            type: "nvarchar(16)",
            maxLength: 16,
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DataVersion",
            table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

        migrationBuilder.DropColumn(
            name: "DataVersion",
            table: "1c_ut_Catalog_СтатьиДвиженияДенежныхСредств");
    }
}
