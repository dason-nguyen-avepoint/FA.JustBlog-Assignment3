using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? userId {  get; set; }
        public ApplicationUser ? Users { get; set; }
        public int postId { get; set; }
        public Posts ? Post { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string PostTitle { get; set; }
    }
}
