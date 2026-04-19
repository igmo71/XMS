using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class RenameToCatalog_СтатьяДДС_Key : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Catalog_СтатьиДвиженияДенежныхСредств_RefKey",
            table: "CostCatalog_ДДС",
            newName: "Catalog_СтатьяДДС_Key");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Catalog_СтатьяДДС_Key",
            table: "CostCatalog_ДДС",
            newName: "Catalog_СтатьиДвиженияДенежныхСредств_RefKey");
    }
}
