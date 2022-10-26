using System.ComponentModel.DataAnnotations;

namespace JobsAPI.Models
{
    public class user
    {
        [Key]
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? Bio { get; set; }//applicant
        public string EmailId { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }

        public string PhotographLink { get; set; }
        public string? ResumeLink { get; set; }//applicant

        public bool? WorkStatus { get; set; }//applicant
        public double? CurrentSalary { get; set; }//applicant
        public double? ExpectedSalary { get; set; }//applicant
        public string? CurrentLocation { get; set; }//applicant
        public string? PreferredLocation { get; set; }//applicant

        public string? CompanyName { get; set; }//recruiter
        public string? RecruiterDescription { get; set; }//recruiter

        public string Role { get; set; } //Either Admin,Recruiter,Applicant

        public ICollection<Blog> Blogs { get; set; } // For Applicants
    }
}
