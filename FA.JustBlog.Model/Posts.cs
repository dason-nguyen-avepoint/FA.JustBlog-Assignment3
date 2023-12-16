using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Model
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
        public bool isPublised { get; set; }
        [NotMapped]
        public int Rate { get; set; }
        [NotMapped]
        public string? CateName { get; set; }
        public Nullable<int> CategoryId { get; set; }
        
        public virtual Category ? Categories { get; set; }
        public virtual IEnumerable<TagPost> ? TagPosts { get; set; }
        public virtual IEnumerable<InterestPost> ? InterestPosts { get; set; }
        public virtual IEnumerable<Comment> ?Comments { get; set; }
    }
}
