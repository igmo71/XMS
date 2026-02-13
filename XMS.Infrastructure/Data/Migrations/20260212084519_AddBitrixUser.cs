using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddBitrixUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BitrixId",
                table: "AspNetUsers",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AspNetUsers",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BitrixId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AspNetUsers");
        }
    }
}
