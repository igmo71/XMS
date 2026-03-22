using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletionMarkAndPosted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "СтатьяРасходов",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ДокументОснование",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Договор",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "DeletionMark",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Posted",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletionMark",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

            migrationBuilder.DropColumn(
                name: "Posted",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

            migrationBuilder.AlterColumn<Guid>(
                name: "СтатьяРасходов",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств_РасшифровкаПлатежа",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ДокументОснование",
                table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Договор",
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
