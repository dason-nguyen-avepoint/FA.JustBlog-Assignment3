using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace NWEB.Final.Core.Models
{
    public class LoaiVacXin
    {
        [Key]
        public string MaLoaiVX { get; set; }
        public string TenLoaiVX { get; set; }
        public string NuocSX { get; set; }
        public int SoNgayTienNhac { get; set; }
        public virtual IEnumerable<LieuVacXin> LieuVacXins { get; set; }
    }
}
