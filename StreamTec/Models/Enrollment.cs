namespace StreamTec.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public List<Stream> StreamF { get; set; }
        public List<Student> StudentF { get; set; }
    }
}
