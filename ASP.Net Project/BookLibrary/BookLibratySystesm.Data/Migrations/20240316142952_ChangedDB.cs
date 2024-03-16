using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibratySystesm.Data.Migrations
{
    public partial class ChangedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("340eea2c-9dd0-4e97-9ac6-e0925efe0773"));

            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Books",
                newName: "FileName");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreatedOn", "Description", "FileName", "Image", "Language", "LikerId", "Pages", "Price", "Publisher", "Title" },
                values: new object[] { new Guid("2fccf1e2-393c-4440-8ad8-3a3923638e01"), new Guid("ca1523ec-643e-44fb-ae25-bae604c1bb9e"), 1, new DateTime(2024, 3, 16, 14, 29, 52, 414, DateTimeKind.Utc).AddTicks(3898), "This is a sample description for Book 1.", "Test", new byte[0], "English", new Guid("15f6ecc5-67f3-4354-b67c-35b9abd8615c"), "300", 2100.00m, "Sample Publisher 1", "Sample Book 1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("2fccf1e2-393c-4440-8ad8-3a3923638e01"));

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Books",
                newName: "AuthorName");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "AuthorName", "CategoryId", "CreatedOn", "Description", "Image", "Language", "LikerId", "Pages", "Price", "Publisher", "Title", "isActive" },
                values: new object[] { new Guid("340eea2c-9dd0-4e97-9ac6-e0925efe0773"), new Guid("ca1523ec-643e-44fb-ae25-bae604c1bb9e"), "Test", 1, new DateTime(2024, 3, 16, 8, 5, 39, 388, DateTimeKind.Utc).AddTicks(7444), "This is a sample description for Book 1.", new byte[0], "English", new Guid("15f6ecc5-67f3-4354-b67c-35b9abd8615c"), "300", "19.99", "Sample Publisher 1", "Sample Book 1", false });
        }
    }
}
