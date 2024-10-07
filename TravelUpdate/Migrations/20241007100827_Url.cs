using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelUpdate.Migrations
{
    /// <inheritdoc />
    public partial class Url : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestUrls",
                columns: table => new
                {
                    RequestUrlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestUrls", x => x.RequestUrlId);
                });

            migrationBuilder.CreateTable(
                name: "UrlServices",
                columns: table => new
                {
                    UrlServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestUrlId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlServices", x => x.UrlServiceId);
                    table.ForeignKey(
                        name: "FK_UrlServices_RequestUrls_RequestUrlId",
                        column: x => x.RequestUrlId,
                        principalTable: "RequestUrls",
                        principalColumn: "RequestUrlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlServices_RequestUrlId",
                table: "UrlServices",
                column: "RequestUrlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlServices");

            migrationBuilder.DropTable(
                name: "RequestUrls");
        }
    }
}
