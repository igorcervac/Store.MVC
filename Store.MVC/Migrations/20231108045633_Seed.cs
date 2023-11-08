using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.MVC.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "storemvc",
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Amber" },
                    { 2, "Dark" },
                    { 3, "Clear" }
                });

            migrationBuilder.InsertData(
                schema: "storemvc",
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "ProductTypeId" },
                values: new object[,]
                {
                    { 1, "Description 1", "Syrup1", 10m, 2 },
                    { 2, "Description 2", "Syrup2", 20m, 3 },
                    { 3, "Description 3", "Syrup3", 30m, 1 },
                    { 4, "Description 4", "Syrup4", 40m, 2 },
                    { 5, "Description 5", "Syrup5", 50m, 3 },
                    { 6, "Description 6", "Syrup6", 60m, 1 },
                    { 7, "Description 7", "Syrup7", 70m, 2 },
                    { 8, "Description 8", "Syrup8", 80m, 3 },
                    { 9, "Description 9", "Syrup9", 90m, 1 },
                    { 10, "Description 10", "Syrup10", 100m, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "storemvc",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
