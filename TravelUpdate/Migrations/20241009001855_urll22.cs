using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelUpdate.Migrations
{
    /// <inheritdoc />
    public partial class urll22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CurrentUrls",
                columns: new[] { "CurrentUrlId", "Title", "Url" },
                values: new object[,]
                {
                    { 1, "API Base URL", "/api" },
                    { 2, "Dashboard", "/dashboard" },
                    { 3, "List Users", "/users" },
                    { 4, "Add User", "/users/add" },
                    { 5, "Edit User", "/users/edit/:id" },
                    { 6, "List Categories", "/categories" },
                    { 7, "Add Category", "/categories/add" },
                    { 8, "Edit Category", "/categories/edit/:id" },
                    { 9, "List Sub Categories", "/sub-categories" },
                    { 10, "Add Sub Category", "/sub-categories/add" },
                    { 11, "Edit Sub Category", "/sub-categories/edit/:id" },
                    { 12, "List Countries", "/countries" },
                    { 13, "Add Country", "/countries/add" },
                    { 14, "Edit Country", "/countries/edit/:id" },
                    { 15, "List States", "/states" },
                    { 16, "Add State", "/states/add" },
                    { 17, "Edit State", "/states/edit/:id" },
                    { 18, "List Packages", "/packages" },
                    { 19, "Add Package", "/packages/add" },
                    { 20, "Edit Package", "/packages/edit/:id" },
                    { 21, "Add Package Details", "/packages/details/add/:id" },
                    { 22, "List Schedules", "/schedules" },
                    { 23, "Add Schedule", "/schedules/add" },
                    { 24, "Edit Schedule", "/schedules/edit/:id" },
                    { 25, "List Tour Vouchers", "/tour-vouchers" },
                    { 26, "Add Tour Voucher", "/tour-vouchers/add" },
                    { 27, "Edit Tour Voucher", "/tour-vouchers/edit/:id" },
                    { 28, "List Students", "/students" },
                    { 29, "Add Student", "/students/add" },
                    { 30, "Edit Student", "/students/edit/:id" },
                    { 31, "More Example Path", "/more/path/example" }
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "CurrentUrls",
                keyColumn: "CurrentUrlId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "RequestUrls",
                keyColumn: "RequestUrlId",
                keyValue: 1);

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
