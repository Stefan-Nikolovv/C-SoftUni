using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibratySystesm.Data.Migrations
{
    public partial class TestingIFormFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("2fccf1e2-393c-4440-8ad8-3a3923638e01"));

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Pages",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pages",
                table: "Books",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Books",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreatedOn", "Description", "FileName", "Image", "Language", "LikerId", "Pages", "Price", "Publisher", "Title", "isActive" },
                values: new object[] { new Guid("2fccf1e2-393c-4440-8ad8-3a3923638e01"), new Guid("ca1523ec-643e-44fb-ae25-bae604c1bb9e"), 1, new DateTime(2024, 3, 16, 14, 29, 52, 414, DateTimeKind.Utc).AddTicks(3898), "This is a sample description for Book 1.", "Test", new byte[0], "English", new Guid("15f6ecc5-67f3-4354-b67c-35b9abd8615c"), "300", 2100.00m, "Sample Publisher 1", "Sample Book 1", false });
        }
    }
}
