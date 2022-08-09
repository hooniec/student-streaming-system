using System.ComponentModel.DataAnnotations;

namespace StreamTec.Models
{
    public class Student
    {
        //Data Annotations for required inputs and correct inputs
        [Required(ErrorMessage = "Student ID is required")]
        //Student ID is the Primary Key
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
        public string? Email { get; set; }
    }
}
