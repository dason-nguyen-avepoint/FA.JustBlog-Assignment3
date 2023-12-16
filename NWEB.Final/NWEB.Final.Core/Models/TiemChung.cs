using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NWEB.Final.Core.Models
{
    public class TiemChung
    {
        [Key]
        public string MaTC { get; set; }
        [Required(ErrorMessage ="Thong tin nay khong duoc de trong")]
        
        public string MaCD { get; set; }
        [Required(ErrorMessage = "Thong tin nay khong duoc de trong")]
        public string MaLieuVX { get; set; }
        [Required(ErrorMessage = "Thong tin nay khong duoc de trong")]
        public DateTime NgayTiemMui1 { get; set; }
        public DateTime? NgayDKTienMui2 { get; set; }
        public DateTime? NgayTiemMui2 { get; set; }
        public string? TrangThai { get; set; }
        public string? GhiChu { get; set; }
        public Congdan ? Congdans { get; set; }
        public LieuVacXin ? LieuVacXins { get; set; }
    }
}
