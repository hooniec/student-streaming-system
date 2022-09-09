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


        public string StudentId { get; set; }
        public string StreamID { get; set; }
    }
}
