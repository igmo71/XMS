using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations;

/// <inheritdoc />
public partial class CreateDocument_РеализацияТоваровУслуг : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "1c_ut_Document_РеализацияТоваровУслуг",
            columns: table => new
            {
                Ref_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DataVersion = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                DeletionMark = table.Column<bool>(type: "bit", nullable: false),
                Number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Posted = table.Column<bool>(type: "bit", nullable: false),
                СуммаДокумента = table.Column<int>(type: "int", precision: 18, scale: 2, nullable: false),
                ХозяйственнаяОперация = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Партнер_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Склад_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_1c_ut_Document_РеализацияТоваровУслуг", x => x.Ref_Key);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "1c_ut_Document_РеализацияТоваровУслуг");
    }
}
