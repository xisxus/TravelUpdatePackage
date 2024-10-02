using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelUpdate.Migrations
{
    /// <inheritdoc />
    public partial class daycost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "ScheduleCumulativeCost",
                table: "Schedule");

            migrationBuilder.AddColumn<int>(
                name: "DayCostCategoryID",
                table: "Schedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DayCostCategory",
                columns: table => new
                {
                    DayCostCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportationCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccommodationCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayCostCategory", x => x.DayCostCategoryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_DayCostCategoryID",
                table: "Schedule",
                column: "DayCostCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_DayCostCategory_DayCostCategoryID",
                table: "Schedule",
                column: "DayCostCategoryID",
                principalTable: "DayCostCategory",
                principalColumn: "DayCostCategoryID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_DayCostCategory_DayCostCategoryID",
                table: "Schedule");

            migrationBuilder.DropTable(
                name: "DayCostCategory");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_DayCostCategoryID",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "DayCostCategoryID",
                table: "Schedule");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ScheduleCumulativeCost",
                table: "Schedule",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
