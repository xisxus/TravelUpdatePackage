using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelUpdate.Migrations
{
    /// <inheritdoc />
    public partial class voucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourVouchers_Packages_PackageID",
                table: "TourVouchers");

            migrationBuilder.AlterColumn<int>(
                name: "PackageID",
                table: "TourVouchers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TourVouchers_Packages_PackageID",
                table: "TourVouchers",
                column: "PackageID",
                principalTable: "Packages",
                principalColumn: "PackageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourVouchers_Packages_PackageID",
                table: "TourVouchers");

            migrationBuilder.AlterColumn<int>(
                name: "PackageID",
                table: "TourVouchers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TourVouchers_Packages_PackageID",
                table: "TourVouchers",
                column: "PackageID",
                principalTable: "Packages",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
