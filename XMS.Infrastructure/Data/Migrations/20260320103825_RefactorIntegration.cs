using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class RefactorIntegration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CashFlowCost");

        migrationBuilder.DropTable(
            name: "Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа");

        migrationBuilder.DropTable(
            name: "SkuInventoryUt");

        migrationBuilder.DropTable(
            name: "CashFlowItems");

        migrationBuilder.DropTable(
            name: "Document_СписаниеБезналичныхДенежныхСредств");

        migrationBuilder.CreateTable(
            name: "1c_ut_Catalog_СтатьиДвиженияДенежныхСредств",
            columns: table => new
            {
                Ref_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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

        migrationBuilder.CreateTable(
            name: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
            columns: table => new
            {
                Ref_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                ХозяйственнаяОперация = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Партнер_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                НазначениеПлатежа = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                Автор_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Организация_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Подразделение_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ДокументОснование = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ДокументОснование_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Договор = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Договор_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                НалогообложениеНДС = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                СтатьяДвиженияДенежныхСредств_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Контрагент_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Валюта_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                СуммаДокумента = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Комментарий = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_1c_ut_Document_СписаниеБезналичныхДенежныхСредств", x => x.Ref_Key);
            });

        migrationBuilder.CreateTable(
            name: "CostCatalogUt",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CostCategoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CatalogUtRefKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CostCatalogUt", x => x.Id);
                table.ForeignKey(
                    name: "FK_CostCatalogUt_CostCategoryItems_CostCategoryItemId",
                    column: x => x.CostCategoryItemId,
                    principalTable: "CostCategoryItems",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
            columns: table => new
            {
                Ref_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LineNumber = table.Column<int>(type: "int", nullable: false),
                Партнер_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                СтатьяРасходов = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                СтатьяРасходов_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                СтатьяДвиженияДенежныхСредств_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Комментарий = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                Подразделение_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                НаправлениеДеятельности_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                СтавкаНДС_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ОбъектРасчетов_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Сумма = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ВалютаВзаиморасчетов_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                СуммаВзаиморасчетов = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа", x => new { x.Ref_Key, x.LineNumber });
                table.ForeignKey(
                    name: "FK_1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа_1c_ut_Document_СписаниеБезналичныхДенежныхСредств_Ref_K~",
                    column: x => x.Ref_Key,
                    principalTable: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                    principalColumn: "Ref_Key",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_CostCatalogUt_CostCategoryItemId",
            table: "CostCatalogUt",
            column: "CostCategoryItemId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "1c_ut_Catalog_СтатьиДвиженияДенежныхСредств");

        migrationBuilder.DropTable(
            name: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа");

        migrationBuilder.DropTable(
            name: "CostCatalogUt");

        migrationBuilder.DropTable(
            name: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

        migrationBuilder.CreateTable(
            name: "CashFlowItems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                IsFolder = table.Column<bool>(type: "bit", nullable: false),
                Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CashFlowItems", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Document_СписаниеБезналичныхДенежныхСредств",
            columns: table => new
            {
                Ref_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Автор_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                Валюта_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                Договор = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                Договор_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ДокументОснование = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                ДокументОснование_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Комментарий = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                Контрагент_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                НазначениеПлатежа = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                НалогообложениеНДС = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Организация_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                Партнер_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                Подразделение_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                СтатьяДвиженияДенежныхСредств_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                СуммаДокумента = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ХозяйственнаяОперация = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Document_СписаниеБезналичныхДенежныхСредств", x => x.Ref_Key);
            });

        migrationBuilder.CreateTable(
            name: "SkuInventoryUt",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                QuantityOnHand = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                ScuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SkuInventoryUt", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "CashFlowCost",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CashFlowItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CostCategoryItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CashFlowCost", x => x.Id);
                table.ForeignKey(
                    name: "FK_CashFlowCost_CashFlowItems_CashFlowItemId",
                    column: x => x.CashFlowItemId,
                    principalTable: "CashFlowItems",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CashFlowCost_CostCategoryItems_CostCategoryItemId",
                    column: x => x.CostCategoryItemId,
                    principalTable: "CostCategoryItems",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
            columns: table => new
            {
                Ref_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                LineNumber = table.Column<int>(type: "int", nullable: false),
                ВалютаВзаиморасчетов_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                Комментарий = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                НаправлениеДеятельности_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                ОбъектРасчетов_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                Партнер_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                Подразделение_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                СтавкаНДС_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                СтатьяДвиженияДенежныхСредств_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                СтатьяРасходов = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                СтатьяРасходов_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Сумма = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                СуммаВзаиморасчетов = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа", x => new { x.Ref_Key, x.LineNumber });
                table.ForeignKey(
                    name: "FK_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа_Document_СписаниеБезналичныхДенежныхСредств_Ref_Key",
                    column: x => x.Ref_Key,
                    principalTable: "Document_СписаниеБезналичныхДенежныхСредств",
                    principalColumn: "Ref_Key",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_CashFlowCost_CashFlowItemId",
            table: "CashFlowCost",
            column: "CashFlowItemId");

        migrationBuilder.CreateIndex(
            name: "IX_CashFlowCost_CostCategoryItemId",
            table: "CashFlowCost",
            column: "CostCategoryItemId");

        migrationBuilder.CreateIndex(
            name: "IX_SkuInventoryUt_ScuId_WarehouseId",
            table: "SkuInventoryUt",
            columns: new[] { "ScuId", "WarehouseId" },
            unique: true);
    }
}
