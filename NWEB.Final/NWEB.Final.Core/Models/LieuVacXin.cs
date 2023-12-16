using System.ComponentModel.DataAnnotations;

namespace NWEB.Final.Core.Models
{
    public class LieuVacXin
    {
        [Key]
        public string MaLieuVX { get; set; }
        public int SoLo { get; set; }
        public string MaLoaiVX { get; set; }
        public DateTime NgaySanXuat { get; set; }
        public DateTime NgayNhap { get; set; }
        public DateTime NgayHetHan { get; set; }
        public LoaiVacXin LoaiVacXins { get; set; }
        public virtual IEnumerable<TiemChung> TiemChungs { get; set; }
    }
}
