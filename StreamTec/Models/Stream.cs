using System.ComponentModel.DataAnnotations;

namespace StreamTec.Models
{
    public class Stream
    {
        //no values in this model class should be null
        //Data annotations for data filtering
        [StringLength(50)]
        [Key]
        public string StreamID { get; set; }
        [StringLength(50)]
        public string Room { get; set; }
        public int Credits { get; set; }
        [StringLength(50)]
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Capacity { get; set; }
        
    }
}
