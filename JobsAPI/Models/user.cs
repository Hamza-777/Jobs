using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JobsAPI.Models
{
    
    public class user
    {
        [Key]
        public int UserID { get; set; }
        [Required,StringLength(50)]
        public string FullName { get; set; }
        [Required,StringLength(10)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string? Bio { get; set; }//applicant
        [Required,EmailAddress,StringLength(50)]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }

        public string? Salt { get; set; }
        [Required,MaxLength(10)]
        public long MobileNumber { get; set; }
        public string? PhotographLink { get; set; }
        public string? ResumeLink { get; set; }//applicant

        public bool? WorkStatus { get; set; }//applicant
        public double? CurrentSalary { get; set; }//applicant
        public double? ExpectedSalary { get; set; }//applicant
        [StringLength(50)]
        public string? CurrentLocation { get; set; }//applicant
        [StringLength(50)]
        public string? PreferredLocation { get; set; }//applicant
        [StringLength(50)]
        public string? CompanyName { get; set; }//recruiter
        [StringLength(100)]
        public string? RecruiterDescription { get; set; }//recruiter
        [Required]
        public string Role { get; set; } //Either Admin,Recruiter,Applicant
        public ICollection<Blog>? Blogs { get; set; } // For Applicants
    }
}
