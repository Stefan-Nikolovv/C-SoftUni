using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibratySystesm.Data.Migrations
{
    public partial class SeedDBWithImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Mystery" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Horror" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Fantasy" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "AuthorName", "CategoryId", "CreatedOn", "Description", "Image", "Language", "LikerId", "Pages", "Price", "Publisher", "Title" },
                values: new object[] { new Guid("340eea2c-9dd0-4e97-9ac6-e0925efe0773"), new Guid("ca1523ec-643e-44fb-ae25-bae604c1bb9e"), "Test", 1, new DateTime(2024, 3, 16, 8, 5, 39, 388, DateTimeKind.Utc).AddTicks(7444), "This is a sample description for Book 1.", new byte[0], "English", new Guid("15f6ecc5-67f3-4354-b67c-35b9abd8615c"), "300", "19.99", "Sample Publisher 1", "Sample Book 1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("340eea2c-9dd0-4e97-9ac6-e0925efe0773"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
