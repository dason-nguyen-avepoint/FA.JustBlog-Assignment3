using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NWEB.Final.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Congdan",
                columns: table => new
                {
                    MaCD = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CMND = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Congdan", x => x.MaCD);
                });

            migrationBuilder.CreateTable(
                name: "LoaiVacXin",
                columns: table => new
                {
                    MaLoaiVX = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenLoaiVX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NuocSX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoNgayTienNhac = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiVacXin", x => x.MaLoaiVX);
                });

            migrationBuilder.CreateTable(
                name: "LieuVacXin",
                columns: table => new
                {
                    MaLieuVX = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoLo = table.Column<int>(type: "int", nullable: false),
                    MaLoaiVX = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgaySanXuat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LieuVacXin", x => x.MaLieuVX);
                    table.ForeignKey(
                        name: "FK_LieuVacXin_LoaiVacXin_MaLoaiVX",
                        column: x => x.MaLoaiVX,
                        principalTable: "LoaiVacXin",
                        principalColumn: "MaLoaiVX",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiemChung",
                columns: table => new
                {
                    MaTC = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaCD = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaLieuVX = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayTiemMui1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayDKTienMui2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTiemMui2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiemChung", x => x.MaTC);
                    table.ForeignKey(
                        name: "FK_TiemChung_Congdan_MaCD",
                        column: x => x.MaCD,
                        principalTable: "Congdan",
                        principalColumn: "MaCD",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TiemChung_LieuVacXin_MaLieuVX",
                        column: x => x.MaLieuVX,
                        principalTable: "LieuVacXin",
                        principalColumn: "MaLieuVX",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LieuVacXin_MaLoaiVX",
                table: "LieuVacXin",
                column: "MaLoaiVX");

            migrationBuilder.CreateIndex(
                name: "IX_TiemChung_MaCD",
                table: "TiemChung",
                column: "MaCD");

            migrationBuilder.CreateIndex(
                name: "IX_TiemChung_MaLieuVX",
                table: "TiemChung",
                column: "MaLieuVX");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiemChung");

            migrationBuilder.DropTable(
                name: "Congdan");

            migrationBuilder.DropTable(
                name: "LieuVacXin");

            migrationBuilder.DropTable(
                name: "LoaiVacXin");
        }
    }
}
