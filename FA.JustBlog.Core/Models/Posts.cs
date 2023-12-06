using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Models
{
    public class Posts
    {
        [Key]
        public int Id { get; set; }
        public string ? Title { get; set; }
        public string ? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ? Content { get; set; }
        public int ViewCount { get; set; } = 0;
        public int CategoryId { get; set; }
        public Category ? Categories { get; set; }
        public virtual IEnumerable<TagPost> ? TagPosts { get; set; }
        public virtual IEnumerable<InterestPost> ? InterestPosts { get; set; }
    }
}
