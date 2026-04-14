using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateCostAllocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostAllocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsAllocated = table.Column<bool>(type: "bit", nullable: false),
                    PaymentVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentVoucherType = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BusinessOperation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaymentPurpose = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    CostCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Catalog_СтатьяДДС_Key = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostAllocations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CostAllocations_CostCategories_CostCategoryId",
                        column: x => x.CostCategoryId,
                        principalTable: "CostCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CostAllocations_CostItems_CostItemId",
                        column: x => x.CostItemId,
                        principalTable: "CostItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CostAllocations_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CostAllocations_Employees_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostAllocations_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostAllocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostAllocations_AuthorId",
                table: "CostAllocations",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CostAllocations_CityId",
                table: "CostAllocations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CostAllocations_CostCategoryId",
                table: "CostAllocations",
                column: "CostCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CostAllocations_CostItemId",
                table: "CostAllocations",
                column: "CostItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CostAllocations_DepartmentId",
                table: "CostAllocations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CostAllocations_LocationId",
                table: "CostAllocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CostAllocations_ManagerId",
                table: "CostAllocations",
                column: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostAllocations");
        }
    }
}
