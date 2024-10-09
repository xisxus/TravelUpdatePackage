using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelUpdate.Migrations
{
    /// <inheritdoc />
    public partial class room : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomSubTypes_RoomTypes_RoomTypeID",
                table: "RoomSubTypes");

            migrationBuilder.DropIndex(
                name: "IX_RoomSubTypes_RoomTypeID",
                table: "RoomSubTypes");

            migrationBuilder.DropColumn(
                name: "RoomTypeID",
                table: "RoomSubTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomTypeID",
                table: "RoomSubTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RoomSubTypes_RoomTypeID",
                table: "RoomSubTypes",
                column: "RoomTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomSubTypes_RoomTypes_RoomTypeID",
                table: "RoomSubTypes",
                column: "RoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
