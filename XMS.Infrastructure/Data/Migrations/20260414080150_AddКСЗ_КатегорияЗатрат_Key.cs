using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class AddКСЗ_КатегорияЗатрат_Key : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "КСЗ_КатегорияЗатрат_Key",
            table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "КСЗ_КатегорияЗатрат_Key",
            table: "1c_ut_Document_РасходныйКассовыйОрдер",
            type: "uniqueidentifier",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "КСЗ_КатегорияЗатрат_Key",
            table: "1c_ut_Document_СписаниеБезналичныхДенежныхСредств");

        migrationBuilder.DropColumn(
            name: "КСЗ_КатегорияЗатрат_Key",
            table: "1c_ut_Document_РасходныйКассовыйОрдер");
    }
}
