using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.JustBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3ec5ab9e-f889-404d-9dd1-eb62d9ad2732", "b74ddd14-6340-4840-95c2-db12554843e9" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e9");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 11, 8, 37, 52, 951, DateTimeKind.Local).AddTicks(2751));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 10, 22, 48, 9, 56, DateTimeKind.Local).AddTicks(4389));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "AccessFailedCount", "Address", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e9", null, 0, null, 0, "51bb0ab7-6ad5-4f45-8125-00e401daf239", "ApplicationUser", "admin@gmail.com", true, false, null, "Owner Blog", "admin@gmail.com", "admin@gmail.com", "AQAAAAIAAYagAAAAEPY45OsjipaTWUUTPJrr3x5LLfq70j8Wr2uHUi15FBWI6OJ/dwbCs6E30CbU50mgSg==", null, "0000000000", false, "ea2a8a14-1b85-4c03-929f-6b51c1767222", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3ec5ab9e-f889-404d-9dd1-eb62d9ad2732", "b74ddd14-6340-4840-95c2-db12554843e9" });
        }
    }
}
