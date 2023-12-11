using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.JustBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialAdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 11, 9, 51, 9, 431, DateTimeKind.Local).AddTicks(9092));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "AccessFailedCount", "Address", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e9", null, 0, null, 0, "e99b44cc-ff4c-4e04-85ec-b5a085b5ea6e", "ApplicationUser", "admin@gmail.com", true, false, null, "Owner Blog", "admin@gmail.com", "admin@gmail.com", "AQAAAAIAAYagAAAAEEXNr6+/2EllPt6X9iWIefh718jY5u4/gyebgz5QuLuqUuwe63eyNgTUig6M6gbwJg==", null, "0000000000", false, "f8fa2bd0-d08b-4a60-8cf7-9ca6d07a6b31", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4582a3e7-b236-4a79-9f6c-929aa61b9ec8", "b74ddd14-6340-4840-95c2-db12554843e9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4582a3e7-b236-4a79-9f6c-929aa61b9ec8", "b74ddd14-6340-4840-95c2-db12554843e9" });

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
    }
}
