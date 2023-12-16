using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NWEB.Final.Core.Models;

namespace NWEB.Final.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Congdan> Congdan { get; set; }
        public DbSet<LoaiVacXin> LoaiVacXin { get; set; }
        public DbSet<LieuVacXin> LieuVacXin { get; set; }
        public DbSet<TiemChung> TiemChung { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //RELATIONSHIP
            modelBuilder.Entity<LieuVacXin>().HasOne(x => x.LoaiVacXins).WithMany(x => x.LieuVacXins).HasForeignKey(x => x.MaLoaiVX);
            modelBuilder.Entity<TiemChung>().HasOne(x => x.LieuVacXins).WithMany(x => x.TiemChungs).HasForeignKey(x => x.MaLieuVX);
            modelBuilder.Entity<TiemChung>().HasOne(x => x.Congdans).WithMany(x => x.TiemChungs).HasForeignKey(x => x.MaCD);

            //SEED DATA
            modelBuilder.Entity<LoaiVacXin>().HasData(
                new LoaiVacXin()
                {
                    MaLoaiVX="VX1",
                    TenLoaiVX="Vacxin1",
                    NuocSX = "Germany",
                    SoNgayTienNhac = 10
                });
            modelBuilder.Entity<LieuVacXin>().HasData(
                new LieuVacXin()
                {
                    MaLieuVX ="MaLieu001",
                    SoLo = 1,
                    MaLoaiVX = "VX1",
                    NgaySanXuat = new DateTime(2020,01,01),
                    NgayNhap = new DateTime(2020, 02, 01),
                    NgayHetHan = new DateTime(2024, 01, 01)
                });
            modelBuilder.Entity<Congdan>().HasData(
                new Congdan()
                {
                    MaCD = "CD01",
                    HoTen = "Nguyen Van A",
                    NgaySinh = new DateTime(1999,01,01),
                    CMND = "132456",
                    SoDienThoai = "11111",
                    DiaChi = "Street 1",
                    Email = "email@gmail.com"
                });
            modelBuilder.Entity<TiemChung>().HasData(
                new TiemChung()
                {
                    MaTC = "TC01",
                    MaCD = "CD01",
                    MaLieuVX = "MaLieu001",
                    NgayTiemMui1 = new DateTime(2020,02,02),
                    NgayDKTienMui2 = new DateTime(2020, 02, 12),
                    NgayTiemMui2 = new DateTime(2020, 02, 12),
                    TrangThai = "Da hoan tat",
                    GhiChu = "Da tiem du"
                }
                );
        }
    }

}
