using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FA.JustBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addInitialAdminAccount3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 10, 22, 40, 32, 236, DateTimeKind.Local).AddTicks(5190));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "271fe844-44ee-4f1b-a959-0b1ca1fec887", "AQAAAAIAAYagAAAAEMVDMn+4o/wMgZM/cg1YGgYw3Jg1IilbUkJFejTSwVoZyM1UpHL7A4/pqi3kDlIUjg==", "92d4cc2a-295b-4f08-8ba3-20b5223b2b25", "admin@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 10, 22, 31, 15, 838, DateTimeKind.Local).AddTicks(2343));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b2bf2ce0-29c1-4701-bdab-46ac2906785b", "AQAAAAIAAYagAAAAEKulPn7LLMJVhZRcBiQPOfkPbPCcUiLkHU9k3YtNGUBD9uz1n2iMgQblTCN1LRNL7g==", "edc5a9e4-044a-4213-b58d-a2f12fae8faf", "Duc Nguyen" });
        }
    }
}
