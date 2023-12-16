using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Model
{
    public class InterestPost
    {
        [Key]
        public int InterestId { get; set; }
        public int Rate { get; set; }
        public int PostId { get; set; }
        public virtual Posts? Post { get; set; }
    }
}
