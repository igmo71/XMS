using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDocument_СписаниеБезналичныхДенежныхСредств : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "СтатьяДвиженияДенежныхСредств_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "СтавкаНДС_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Подразделение_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Партнер_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ОбъектРасчетов_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "НаправлениеДеятельности_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ВалютаВзаиморасчетов_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ЗаявкаНаРасходованиеДенежныхСредств",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ЗаявкаНаРасходованиеДенежныхСредств_Type",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Контрагент_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "СтатьяДвиженияДенежныхСредств_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Подразделение_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Партнер_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Организация_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Контрагент_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Валюта_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Автор_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ЗаявкаНаРасходованиеДенежныхСредств",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ЗаявкаНаРасходованиеДенежныхСредств_Type",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "НаправлениеДеятельности_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ОбъектРасчетов_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ЗаявкаНаРасходованиеДенежныхСредств",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа");

            migrationBuilder.DropColumn(
                name: "ЗаявкаНаРасходованиеДенежныхСредств_Type",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа");

            migrationBuilder.DropColumn(
                name: "Контрагент_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа");

            migrationBuilder.DropColumn(
                name: "ЗаявкаНаРасходованиеДенежныхСредств",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

            migrationBuilder.DropColumn(
                name: "ЗаявкаНаРасходованиеДенежныхСредств_Type",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

            migrationBuilder.DropColumn(
                name: "НаправлениеДеятельности_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

            migrationBuilder.DropColumn(
                name: "ОбъектРасчетов_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

            migrationBuilder.AlterColumn<Guid>(
                name: "СтатьяДвиженияДенежныхСредств_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "СтавкаНДС_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Подразделение_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Партнер_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ОбъектРасчетов_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "НаправлениеДеятельности_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ВалютаВзаиморасчетов_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "СтатьяДвиженияДенежныхСредств_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Подразделение_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Партнер_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Организация_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Контрагент_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Валюта_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Автор_Key",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
