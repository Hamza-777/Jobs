using System.ComponentModel.DataAnnotations;

namespace JobsAPI.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }

        public string description { get; set; }
        public string company { get; set; }
        public string category { get; set; }

        public decimal salary { get; set; }
        public string redirect_url { get; set; }
        public DateTime? created_at { get; set; }
        public string location { get; set; }
    }
}
