using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class CreateDocument_СписаниеБезналичныхДенежныхСредств : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
            columns: table => new
            {
                Ref_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DataVersion = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                DeletionMark = table.Column<bool>(type: "bit", nullable: false),
                Posted = table.Column<bool>(type: "bit", nullable: false),
                Number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                СуммаДокумента = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                СтатьяДвиженияДенежныхСредств_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Организация_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Подразделение_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Автор_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Партнер_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Контрагент_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                НаправлениеДеятельности_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ОбъектРасчетов_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Валюта_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ЗаявкаНаРасходованиеДенежныхСредств = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ЗаявкаНаРасходованиеДенежныхСредств_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ДокументОснование = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ДокументОснование_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Договор = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Договор_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ХозяйственнаяОперация = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                НазначениеПлатежа = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                НалогообложениеНДС = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Комментарий = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_1c_ut_Document_СписаниеБезналичныхДенежныхСредств", x => x.Ref_Key);
            });

        migrationBuilder.CreateTable(
            name: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
            columns: table => new
            {
                Ref_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                LineNumber = table.Column<int>(type: "int", nullable: false),
                Сумма = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                СуммаВзаиморасчетов = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                ВалютаВзаиморасчетов_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                СтатьяДвиженияДенежныхСредств_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Подразделение_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Партнер_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Контрагент_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                НаправлениеДеятельности_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ОбъектРасчетов_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                СтавкаНДС_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                СуммаНДС = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                ЗаявкаНаРасходованиеДенежныхСредств = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ЗаявкаНаРасходованиеДенежныхСредств_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                СтатьяРасходов = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                СтатьяРасходов_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Комментарий = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа");

        migrationBuilder.DropTable(
            name: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств");
    }
}
