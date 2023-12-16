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
        public virtual Posts Post { get; set; }
        public virtual Tag Tags { get; set; }
    }
}
