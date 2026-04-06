using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class CreateCatalog_Партнеры : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "1c_ut_Catalog_Партнеры",
            columns: table => new
            {
                Ref_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DataVersion = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                DeletionMark = table.Column<bool>(type: "bit", nullable: false),
                Parent_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                ОсновнойМенеджер_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                БизнесРегион_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ДатаРегистрации = table.Column<DateTime>(type: "datetime2", nullable: false),
                ЮрФизЛицо = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                Комментарий = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                Клиент = table.Column<bool>(type: "bit", nullable: false),
                Поставщик = table.Column<bool>(type: "bit", nullable: false),
                Конкурент = table.Column<bool>(type: "bit", nullable: false),
                Перевозчик = table.Column<bool>(type: "bit", nullable: false),
                ПрочиеОтношения = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_1c_ut_Catalog_Партнеры", x => x.Ref_Key);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "1c_ut_Catalog_Партнеры");
    }
}
