using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Model
{
    public class TagPost
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
        public Posts Post { get; set; }
        public Tag Tags { get; set; }
    }
}
