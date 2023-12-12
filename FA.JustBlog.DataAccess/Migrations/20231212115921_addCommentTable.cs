using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FA.JustBlog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Posts_postId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_UsersId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UsersId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_postId",
                table: "Comments",
                newName: "IX_Comments_postId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "Title", "postId", "userId" },
                values: new object[,]
                {
                    { 1, "Content comment post 1", "Comment Post 1", 1, "4ff02d5b-4ced-49f4-9cee-790d15c687df" },
                    { 2, "Content comment post 2", "Comment Post 2", 2, "4ff02d5b-4ced-49f4-9cee-790d15c687df" },
                    { 3, "Content comment post 3", "Comment Post 3", 3, "4ff02d5b-4ced-49f4-9cee-790d15c687df" }
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 12, 18, 59, 20, 846, DateTimeKind.Local).AddTicks(9624));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eadbdc34-e7fe-4c67-a69a-6e0dc8edfafe", "AQAAAAIAAYagAAAAENDH6ee1bzrzlH6ntBzpBzrDKx7JDIz4KoBWYiX81wXw0KKuqwz/A0s8nGzNH7ZWxA==", "ddc00450-8f76-4d46-811f-3302830172a1" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_userId",
                table: "Comments",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_postId",
                table: "Comments",
                column: "postId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_userId",
                table: "Comments",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_postId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_userId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_userId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_postId",
                table: "Comment",
                newName: "IX_Comment_postId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 12, 18, 40, 57, 765, DateTimeKind.Local).AddTicks(2850));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54eb7fe2-e62d-4f79-a104-709bf7c40723", "AQAAAAIAAYagAAAAENunVzqvjACrdehRuXRktdkIdqBh/4t13eY9dR0PneXWyB9Rtd/rDpvKwABFwuG+MQ==", "9900ff97-97c5-439f-a3b7-ff318f479f96" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UsersId",
                table: "Comment",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Posts_postId",
                table: "Comment",
                column: "postId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_UsersId",
                table: "Comment",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
