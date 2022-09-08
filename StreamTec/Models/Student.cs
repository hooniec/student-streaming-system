using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace StreamTec.Models
{
    public class Student
    {
        //Student ID is the Primary Key
        [Required(ErrorMessage = "Student ID is required")]
        
        //[Range(999999, 9999999, ErrorMessage = "Student ID must be 7 Characters")]        
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
        public string? Email { get; set; }
    }
}
