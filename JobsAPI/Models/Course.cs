using System.ComponentModel.DataAnnotations;

namespace JobsAPI.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCategory { get; set; }
        public string CourseDescription { get; set; }
        public string CourseAuthor { get; set; }
        public decimal CourseAmount { get; set; }

    }
}
