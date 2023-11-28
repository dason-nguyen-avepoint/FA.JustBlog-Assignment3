using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.JustBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBPosts3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most." +
                "This year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone.");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.</br>This year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone.");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description" },
                values: new object[] { new DateTime(2023, 11, 28, 13, 2, 11, 266, DateTimeKind.Local).AddTicks(6476), "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.</br>This year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\\n This year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone.");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone.");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Description" },
                values: new object[] { new DateTime(2023, 11, 28, 12, 59, 52, 169, DateTimeKind.Local).AddTicks(8780), "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\r\n\r\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone." });
        }
    }
}
