using System.ComponentModel.DataAnnotations;

namespace StreamTec.Models
{
    public class Student
    {
        //Student ID is the Primary Key
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
        public string? Email { get; set; }
    }
}
