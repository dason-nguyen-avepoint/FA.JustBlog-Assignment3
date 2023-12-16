using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace NWEB.Final.Core.Models
{
    public class Congdan
    {
        [Key]
        public string MaCD { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CMND { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<TiemChung> TiemChungs { get; set; }
    }
}
