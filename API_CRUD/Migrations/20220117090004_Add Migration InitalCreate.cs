using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_CRUD.Migrations
{
    public partial class AddMigrationInitalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Description", "Name", "Slug", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 17, 12, 0, 4, 101, DateTimeKind.Local).AddTicks(2396), null, "Best boxing gloves are Everlast..!", "Boxing Gloves", "boxing-gloves", 1, null },
                    { 2, new DateTime(2022, 1, 17, 12, 0, 4, 102, DateTimeKind.Local).AddTicks(5974), null, "Best protective equipment are Everlast..!", "Protective Equipment", "protective-equipment", 1, null },
                    { 3, new DateTime(2022, 1, 17, 12, 0, 4, 102, DateTimeKind.Local).AddTicks(6041), null, "Best head gear are Everlast..!", "Head Gear", "head-gear", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Password", "Status", "UpdateDate", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 17, 12, 0, 4, 105, DateTimeKind.Local).AddTicks(1967), null, "123", 1, null, "beast" },
                    { 2, new DateTime(2022, 1, 17, 12, 0, 4, 105, DateTimeKind.Local).AddTicks(2890), null, "123", 1, null, "savage" },
                    { 3, new DateTime(2022, 1, 17, 12, 0, 4, 105, DateTimeKind.Local).AddTicks(2928), null, "123", 1, null, "bear" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
