using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelUpdate.Migrations
{
    /// <inheritdoc />
    public partial class REMOVEID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GalleryID",
                table: "LocationGalleries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GalleryID",
                table: "LocationGalleries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
