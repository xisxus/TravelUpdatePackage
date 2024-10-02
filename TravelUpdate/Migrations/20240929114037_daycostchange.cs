using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelUpdate.Migrations
{
    /// <inheritdoc />
    public partial class daycostchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccomodationCost",
                table: "DayWiseTourCosts");

            migrationBuilder.DropColumn(
                name: "CostCategory",
                table: "DayWiseTourCosts");

            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "DayWiseTourCosts");

            migrationBuilder.DropColumn(
                name: "FoodCost",
                table: "DayWiseTourCosts");

            migrationBuilder.DropColumn(
                name: "TransPortCost",
                table: "DayWiseTourCosts");

            migrationBuilder.AddColumn<int>(
                name: "DayCostCategoryID",
                table: "DayWiseTourCosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DayWiseTourCosts_DayCostCategoryID",
                table: "DayWiseTourCosts",
                column: "DayCostCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_DayWiseTourCosts_DayCostCategory_DayCostCategoryID",
                table: "DayWiseTourCosts",
                column: "DayCostCategoryID",
                principalTable: "DayCostCategory",
                principalColumn: "DayCostCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayWiseTourCosts_DayCostCategory_DayCostCategoryID",
                table: "DayWiseTourCosts");

            migrationBuilder.DropIndex(
                name: "IX_DayWiseTourCosts_DayCostCategoryID",
                table: "DayWiseTourCosts");

            migrationBuilder.DropColumn(
                name: "DayCostCategoryID",
                table: "DayWiseTourCosts");

            migrationBuilder.AddColumn<decimal>(
                name: "AccomodationCost",
                table: "DayWiseTourCosts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CostCategory",
                table: "DayWiseTourCosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DayNumber",
                table: "DayWiseTourCosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "FoodCost",
                table: "DayWiseTourCosts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransPortCost",
                table: "DayWiseTourCosts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
