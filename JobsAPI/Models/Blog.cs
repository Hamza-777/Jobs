using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsAPI.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [StringLength(50)]
        public string BlogTitle { get; set; }

        [StringLength(100)]
        public string? BlogDescription { get; set; }

        [Required]
        public string BlogContent { get; set; }

        [Required]
        [StringLength(50)]
        public string BlogCategory { get; set; }

        public string? BlogTags { get; set; }

        [StringLength(100)]
        public string? Company { get; set; } // For experience category

        // Foreign Key reference to user table
        public int UserID { get; set; }

        public user? Author { get; set; }
    }
}
