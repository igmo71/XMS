using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class RenameOperationalManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_OperationManagerId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "OperationManagerId",
                table: "Employees",
                newName: "OperationalManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_OperationManagerId",
                table: "Employees",
                newName: "IX_Employees_OperationalManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_OperationalManagerId",
                table: "Employees",
                column: "OperationalManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_OperationalManagerId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "OperationalManagerId",
                table: "Employees",
                newName: "OperationManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_OperationalManagerId",
                table: "Employees",
                newName: "IX_Employees_OperationManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_OperationManagerId",
                table: "Employees",
                column: "OperationManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
