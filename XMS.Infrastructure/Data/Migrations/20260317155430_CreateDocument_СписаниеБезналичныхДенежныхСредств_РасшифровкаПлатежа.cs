using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateDocument_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Document_СписаниеБезналичныхДенежныхСредств",
                columns: table => new
                {
                    Ref_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ХозяйственнаяОперация = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Партнер_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    НазначениеПлатежа = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Автор_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Организация_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Подразделение_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ДокументОснование = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ДокументОснование_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Договор = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Договор_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    НалогообложениеНДС = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    СтатьяДвиженияДенежныхСредств_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Контрагент_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Валюта_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    СуммаДокумента = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Комментарий = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document_СписаниеБезналичныхДенежныхСредств", x => x.Ref_Key);
                });

            migrationBuilder.CreateTable(
                name: "Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                columns: table => new
                {
                    Ref_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    Партнер_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    СтатьяРасходов = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    СтатьяРасходов_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    СтатьяДвиженияДенежныхСредств_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Комментарий = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Подразделение_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    НаправлениеДеятельности_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    СтавкаНДС_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ОбъектРасчетов_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Сумма = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ВалютаВзаиморасчетов_Key = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа");

            migrationBuilder.DropTable(
                name: "Document_СписаниеБезналичныхДенежныхСредств");
        }
    }
}
