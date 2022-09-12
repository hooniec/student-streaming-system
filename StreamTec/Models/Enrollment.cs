using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamTec.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public virtual Student Students { get; set; }
        public virtual Stream Streams { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid Student ID")]
        [MinLength(7, ErrorMessage = "Must be 7 Characters long")]
        [MaxLength(7, ErrorMessage = "Must be 7 Characters long")]
        public string StudentId { get; set; }

        public string StreamID { get; set; }
    }
}
