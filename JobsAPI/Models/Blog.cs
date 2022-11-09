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

        [Required]
        public string BlogContent { get; set; }

        public string? BlogTags { get; set; }

        // Foreign Key reference to user table
        public int UserID { get; set; }

        public user? Author { get; set; }
    }
}
