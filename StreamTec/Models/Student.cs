using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace StreamTec.Models
{
    public class Student
    {
        //Student ID is the Primary Key
        [Required(ErrorMessage = "Student ID is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Student ID")]
        [MinLength(7, ErrorMessage = "Must be 7 Characters long")]
        [MaxLength(7, ErrorMessage = "Must be 7 Characters long")]
        [Key]
        
        //[Range(999999, 9999999, ErrorMessage = "Student ID must be 7 Characters")]             
        public string StudentId { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
