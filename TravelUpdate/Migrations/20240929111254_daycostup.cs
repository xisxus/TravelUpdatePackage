using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelUpdate.Migrations
{
    /// <inheritdoc />
    public partial class daycostup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccommodationCost",
                table: "DayCostCategory");

            migrationBuilder.DropColumn(
                name: "FoodCost",
                table: "DayCostCategory");

            migrationBuilder.DropColumn(
                name: "OtherCost",
                table: "DayCostCategory");

            migrationBuilder.DropColumn(
                name: "TransportationCost",
                table: "DayCostCategory");

            migrationBuilder.AddColumn<string>(
                name: "DayCostCategoryName",
                table: "DayCostCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayCostCategoryName",
                table: "DayCostCategory");

            migrationBuilder.AddColumn<decimal>(
                name: "AccommodationCost",
                table: "DayCostCategory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FoodCost",
                table: "DayCostCategory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OtherCost",
                table: "DayCostCategory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransportationCost",
                table: "DayCostCategory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
