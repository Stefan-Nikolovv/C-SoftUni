using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibratySystesm.Data.Migrations
{
    public partial class TEst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Books",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Books",
                newName: "FileName");
        }
    }
}
