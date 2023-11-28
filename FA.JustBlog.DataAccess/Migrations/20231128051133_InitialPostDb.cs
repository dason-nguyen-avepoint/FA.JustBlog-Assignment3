using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FA.JustBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CreatedDate", "Description", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\r\n\r\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone.", "Hello, this my my first post!" },
                    { 2, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\r\n\r\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone.", "Hello, this my my first post!" },
                    { 3, new DateTime(2023, 11, 28, 12, 11, 33, 85, DateTimeKind.Local).AddTicks(33), "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\r\n\r\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone.", "Hello, this my my first post!" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
