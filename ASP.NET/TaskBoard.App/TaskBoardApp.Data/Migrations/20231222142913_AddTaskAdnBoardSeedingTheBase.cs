using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoard.App.Data.Migrations
{
    public partial class AddTaskAdnBoardSeedingTheBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Open" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "In Progress" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Done" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { new Guid("5fe90885-071f-478e-93fb-e743cfb26a1a"), 1, new DateTime(2023, 7, 22, 14, 29, 13, 704, DateTimeKind.Utc).AddTicks(7295), "Create Android client App for the RESTful TaskBoard service", "b671e676-ad8b-47aa-bc1e-a50f9bac4966", "Android Client App" },
                    { new Guid("620f6cb3-bdd6-4047-91e6-9beca7d1cd66"), 1, new DateTime(2023, 6, 5, 14, 29, 13, 704, DateTimeKind.Utc).AddTicks(7269), "Implement better styling for all public pages", "1581772a-fbe7-465d-96cc-32c5f2d9365e", "Improve CSS styles" },
                    { new Guid("dd318c0d-ec07-4ab9-bd44-c2373a29a89a"), 3, new DateTime(2022, 12, 22, 14, 29, 13, 704, DateTimeKind.Utc).AddTicks(7303), "Implement [Create Task] page for adding tasks", "b671e676-ad8b-47aa-bc1e-a50f9bac4966", "Create Tasks" },
                    { new Guid("ebce73ad-652f-4c12-ac61-b4f9574ce174"), 2, new DateTime(2023, 11, 22, 14, 29, 13, 704, DateTimeKind.Utc).AddTicks(7300), "Create Desktop client App for the RESTful TaskBoard service", "b671e676-ad8b-47aa-bc1e-a50f9bac4966", "Desktop Client App" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
