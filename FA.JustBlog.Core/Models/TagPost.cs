using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Models
{
    public class TagPost
    {
        [Key]
        public int PostId { get; set; }
        [Key]
        public int TagId { get; set; }
        public Posts Post { get; set; }
        public Tag Tags { get; set; }
    }
}
