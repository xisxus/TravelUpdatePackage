using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelUpdate.Migrations
{
    /// <inheritdoc />
    public partial class init11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RequestUrls",
                columns: new[] { "RequestUrlId", "Url", "UrlName" },
                values: new object[,]
                {
                    { 1, "/api", "API Base URL" },
                    { 2, "/dashboard", "Dashboard" },
                    { 3, "/users", "List Users" },
                    { 4, "/users/add", "Add User" },
                    { 5, "/users/edit/:id", "Edit User" },
                    { 6, "/categories", "List Categories" },
                    { 7, "/categories/add", "Add Category" },
                    { 8, "/categories/edit/:id", "Edit Category" },
                    { 9, "/sub-categories", "List Sub Categories" },
                    { 10, "/sub-categories/add", "Add Sub Category" },
                    { 11, "/sub-categories/edit/:id", "Edit Sub Category" },
                    { 12, "/countries", "List Countries" },
                    { 13, "/countries/add", "Add Country" },
                    { 14, "/countries/edit/:id", "Edit Country" },
                    { 15, "/states", "List States" },
                    { 16, "/states/add", "Add State" },
                    { 17, "/states/edit/:id", "Edit State" },
                    { 18, "/packages", "List Packages" },
                    { 19, "/packages/add", "Add Package" },
                    { 20, "/packages/edit/:id", "Edit Package" },
                    { 21, "/packages/details/add/:id", "Add Package Details" },
                    { 22, "/schedules", "List Schedules" },
                    { 23, "/schedules/add", "Add Schedule" },
                    { 24, "/schedules/edit/:id", "Edit Schedule" },
                    { 25, "/tour-vouchers", "List Tour Vouchers" },
                    { 26, "/tour-vouchers/add", "Add Tour Voucher" },
                    { 27, "/tour-vouchers/edit/:id", "Edit Tour Voucher" },
                    { 28, "/students", "List Students" },
                    { 29, "/students/add", "Add Student" },
                    { 30, "/students/edit/:id", "Edit Student" },
                    { 31, "/more/path/example", "More Example Path" }
                });

            migrationBuilder.InsertData(
                table: "UrlServices",
                columns: new[] { "UrlServiceId", "CurrentUrl", "Description", "RequestUrlId" },
                values: new object[,]
                {
                    { 1, "/dashboard", "Dashboard", 2 },
                    { 2, "/users", "List Users", 3 },
                    { 3, "/users/add", "Add User", 4 },
                    { 4, "/users/edit/:id", "Edit User", 5 },
                    { 5, "/categories", "List Categories", 6 },
                    { 6, "/categories/add", "Add Category", 7 },
                    { 7, "/categories/edit/:id", "Edit Category", 8 },
                    { 8, "/sub-categories", "List Sub Categories", 9 },
                    { 9, "/sub-categories/add", "Add Sub Category", 10 },
                    { 10, "/sub-categories/edit/:id", "Edit Sub Category", 11 },
                    { 11, "/countries", "List Countries", 12 },
                    { 12, "/countries/add", "Add Country", 13 },
                    { 13, "/countries/edit/:id", "Edit Country", 14 },
                    { 14, "/states", "List States", 15 },
                    { 15, "/states/add", "Add State", 16 },
                    { 16, "/states/edit/:id", "Edit State", 17 },
                    { 17, "/packages", "List Packages", 18 },
                    { 18, "/packages/add", "Add Package", 19 },
                    { 19, "/packages/edit/:id", "Edit Package", 20 },
                    { 20, "/packages/details/add/:id", "Add Package Details", 21 },
                    { 21, "/schedules", "List Schedules", 22 },
                    { 22, "/schedules/add", "Add Schedule", 23 },
                    { 23, "/schedules/edit/:id", "Edit Schedule", 24 },
                    { 24, "/tour-vouchers", "List Tour Vouchers", 25 },
                    { 25, "/tour-vouchers/add", "Add Tour Voucher", 26 },
                    { 26, "/tour-vouchers/edit/:id", "Edit Tour Voucher", 27 },
                    { 27, "/students", "List Students", 28 },
                    { 28, "/students/add", "Add Student", 29 },
                    { 29, "/students/edit/:id", "Edit Student", 30 },
                    { 30, "/more/path/example", "More Example Path", 31 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "UrlServices",
                keyColumn: "UrlServiceId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 31);
        }
    }
}
