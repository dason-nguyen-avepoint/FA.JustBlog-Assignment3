using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Model
{
    public class RegisterModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string ? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? FullName { get; set; }
        [Required]
        [Range(18,60)]
        public int Age { get; set; }
        public string? Address { get; set; }
    }
}
