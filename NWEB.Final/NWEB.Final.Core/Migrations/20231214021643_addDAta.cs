using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NWEB.Final.Core.Migrations
{
    /// <inheritdoc />
    public partial class addDAta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Congdan",
                columns: new[] { "MaCD", "CMND", "DiaChi", "Email", "HoTen", "NgaySinh", "SoDienThoai" },
                values: new object[] { "CD01", "132456", "Street 1", "email@gmail.com", "Nguyen Van A", new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "11111" });

            migrationBuilder.InsertData(
                table: "LoaiVacXin",
                columns: new[] { "MaLoaiVX", "NuocSX", "SoNgayTienNhac", "TenLoaiVX" },
                values: new object[] { "VX1", "Germany", 10, "Vacxin1" });

            migrationBuilder.InsertData(
                table: "LieuVacXin",
                columns: new[] { "MaLieuVX", "MaLoaiVX", "NgayHetHan", "NgayNhap", "NgaySanXuat", "SoLo" },
                values: new object[] { "MaLieu001", "VX1", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "TiemChung",
                columns: new[] { "MaTC", "GhiChu", "MaCD", "MaLieuVX", "NgayDKTienMui2", "NgayTiemMui1", "NgayTiemMui2", "TrangThai" },
                values: new object[] { "TC01", "Da tiem du", "CD01", "MaLieu001", new DateTime(2020, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Da hoan tat" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TiemChung",
                keyColumn: "MaTC",
                keyValue: "TC01");

            migrationBuilder.DeleteData(
                table: "Congdan",
                keyColumn: "MaCD",
                keyValue: "CD01");

            migrationBuilder.DeleteData(
                table: "LieuVacXin",
                keyColumn: "MaLieuVX",
                keyValue: "MaLieu001");

            migrationBuilder.DeleteData(
                table: "LoaiVacXin",
                keyColumn: "MaLoaiVX",
                keyValue: "VX1");
        }
    }
}
