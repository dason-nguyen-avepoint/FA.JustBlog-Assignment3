using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.JustBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBPosts9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "This my my second post!");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2023, 11, 28, 13, 30, 44, 571, DateTimeKind.Local).AddTicks(2653), "This my my third post!" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Hello, this my my first post!");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Title" },
                values: new object[] { new DateTime(2023, 11, 28, 13, 28, 30, 869, DateTimeKind.Local).AddTicks(2149), "Hello, this my my first post!" });
        }
    }
}
