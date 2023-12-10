using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.JustBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addInitialAdminAccount2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3ec5ab9e-f889-404d-9dd1-eb62d9ad2732", "b74ddd14-6340-4840-95c2-db12554843e5" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 10, 22, 31, 15, 838, DateTimeKind.Local).AddTicks(2343));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "AccessFailedCount", "Address", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e6", null, 0, null, 0, "b2bf2ce0-29c1-4701-bdab-46ac2906785b", "ApplicationUser", "admin@gmail.com", true, false, null, "Owner Blog", null, null, "AQAAAAIAAYagAAAAEKulPn7LLMJVhZRcBiQPOfkPbPCcUiLkHU9k3YtNGUBD9uz1n2iMgQblTCN1LRNL7g==", null, "0000000000", false, "edc5a9e4-044a-4213-b58d-a2f12fae8faf", false, "Duc Nguyen" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3ec5ab9e-f889-404d-9dd1-eb62d9ad2732", "b74ddd14-6340-4840-95c2-db12554843e6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3ec5ab9e-f889-404d-9dd1-eb62d9ad2732", "b74ddd14-6340-4840-95c2-db12554843e6" });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e6");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 10, 22, 22, 32, 470, DateTimeKind.Local).AddTicks(192));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "AccessFailedCount", "Address", "Age", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e5", null, 0, null, 0, "67dfa50f-519a-481f-bcfd-1e32706939fc", "ApplicationUser", "duc@gmail.com", true, false, null, "Owner Blog", null, null, null, null, "0000000000", false, "0b5b2b15-c729-42eb-95d5-43f554d669dc", false, "Admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3ec5ab9e-f889-404d-9dd1-eb62d9ad2732", "b74ddd14-6340-4840-95c2-db12554843e5" });
        }
    }
}
