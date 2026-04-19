using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class CreateCatalog_СтатьиДвиженияДенежныхСредств : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "1c_ut_Catalog_СтатьиДвиженияДенежныхСредств",
            columns: table => new
            {
                Ref_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DataVersion = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                DeletionMark = table.Column<bool>(type: "bit", nullable: false),
                Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                Parent_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsFolder = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_1c_ut_Catalog_СтатьиДвиженияДенежныхСредств", x => x.Ref_Key);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "1c_ut_Catalog_СтатьиДвиженияДенежныхСредств");
    }
}
