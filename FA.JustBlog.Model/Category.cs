using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string ? Name { get; set; }
        public virtual IEnumerable<Posts> ? Posts { get; set; }
    }
}
