using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string ? Name { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Format Email. Please try again!")]
        public string ? Email { get; set; }
        [Required]
        [Range(16, 100)]
        public int Age { get; set; }
        [Required]
        [RegularExpression("^.{9}$|^.{10}$", ErrorMessage = "Phone must have 9/10 characters")]

        public string ? PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^(0[1-9]|[1-2][0-9]|3[0-1])/(0[1-9]|1[0-2])/\d{4}$", ErrorMessage = "Invalid date format. Please use dd/MM/yyyy.")]
        public DateTime StartDate { get; set; }
        [Required]
        [RegularExpression(@"^(0[1-9]|[1-2][0-9]|3[0-1])/(0[1-9]|1[0-2])/\d{4}$", ErrorMessage = "Invalid date format. Please use dd/MM/yyyy.")]
        public DateTime EndDate { get; set; }
    }
}
