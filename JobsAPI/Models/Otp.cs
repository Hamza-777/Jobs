using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

namespace JobsAPI.Models
{
    public class Otp
    {
        [Key]
        public int Otpid { get; set; }
        public string value { get; set; }
        public DateTime Timestamp { get; set; }

        public Otp(string value)
        {
            this.value = value.Trim();
            Timestamp = DateTime.Now;
        }
        public Otp()
        {

        }
    }
}
