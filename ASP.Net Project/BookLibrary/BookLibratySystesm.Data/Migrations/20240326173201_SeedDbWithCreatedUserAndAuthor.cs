using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibratySystesm.Data.Migrations
{
    public partial class SeedDbWithCreatedUserAndAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreatedOn", "Description", "Image", "Language", "LikerId", "Pages", "Price", "Publisher", "Title" },
                values: new object[] { new Guid("001deb00-c049-414c-828d-8994b8dad751"), new Guid("430e99fb-5a76-4235-9b39-83b13b17bb58"), 2, new DateTime(2024, 3, 26, 17, 32, 1, 630, DateTimeKind.Utc).AddTicks(8993), "Everything can be taken from a man but one thing: the last of the human freedoms—to choose one’s attitude in any given set of circumstances, to choose", "107aba19-e84b-41e9-95bc-fefb811b9576_Man's Search for Meaning.jpg", "German", new Guid("794405d2-c399-41d9-9a60-c4e2e7cb1b67"), "233", 2100.00m, "amazonPrime", "Man's Search for Meaning" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreatedOn", "Description", "Image", "Language", "LikerId", "Pages", "Price", "Publisher", "Title" },
                values: new object[] { new Guid("b53c3cef-bd0b-4ef8-a62b-48566925ad90"), new Guid("430e99fb-5a76-4235-9b39-83b13b17bb58"), 4, new DateTime(2024, 3, 26, 17, 32, 1, 630, DateTimeKind.Utc).AddTicks(8997), "Is 14 Peaks a book? Beyond Possible: One Man, Fourteen Peaks, and the Mountaineering Achievement of a Lifetime by ", "8ca6ff06-ac52-47bc-b625-7f9355953c69_Beyond Possible.jpg", "English", new Guid("794405d2-c399-41d9-9a60-c4e2e7cb1b67"), "245", 2100.00m, "NetFlixPrime", "Beyond Possible" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Biography" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CreatedOn", "Description", "Image", "Language", "LikerId", "Pages", "Price", "Publisher", "Title" },
                values: new object[] { new Guid("4ee91fef-3f6b-43c5-8f49-6719172818f0"), new Guid("430e99fb-5a76-4235-9b39-83b13b17bb58"), 3, new DateTime(2024, 3, 26, 17, 32, 1, 630, DateTimeKind.Utc).AddTicks(8982), "Book created about the story of Bulgarian mountain climber. ", "2a46eb9a-6e55-42d3-8827-8059333c4b53_The First Seven.jpg", "Bulgarian", new Guid("794405d2-c399-41d9-9a60-c4e2e7cb1b67"), "245", 2100.00m, "VackonPrime", "The First Seven" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("001deb00-c049-414c-828d-8994b8dad751"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("4ee91fef-3f6b-43c5-8f49-6719172818f0"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("b53c3cef-bd0b-4ef8-a62b-48566925ad90"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
