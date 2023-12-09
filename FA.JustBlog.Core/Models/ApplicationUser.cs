using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Core.Models
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
    }
}
