using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Model
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string ? Name { get; set; }
        [Range(18,60)]
        public int Age { get; set; }
        public string ?  AboutMe { get; set; }
        public string ? Address { get; set; }
        public string ? Phone { get; set; }
        public IEnumerable<Comment>? Comments { get; set; }
    }
}
