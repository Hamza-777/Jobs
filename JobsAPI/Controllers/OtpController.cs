using JobsAPI.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using NuGet.Protocol;
using JobsAPI.Hashing;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using System.Configuration;
namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly userDbContext db;

        private IConfiguration configuration;
        public OtpController(IConfiguration iConfig, userDbContext _db, HashMethods _hm)
        {
            configuration = iConfig;
            db = _db;
        }
        [HttpGet("clearotp")]
        public async Task<ActionResult<string>> ClearOTP()
        {
            List<Otp> invalidotps = db.Otps.Where(x => x.Timestamp.AddMinutes(2) < DateTime.Now).ToList();
            db.Otps.RemoveRange(invalidotps);
            await db.SaveChangesAsync();
            return Ok(invalidotps.ToJson());
        }
        [HttpGet("checkotp")]
        public async Task<ActionResult<string>> CheckOTP(string value)
        {
            Otp otp = await db.Otps.Where(x => x.value == value).SingleOrDefaultAsync();
            await ClearOTP();
            if (otp != null)
            {
                if (otp.Timestamp.AddMinutes(2) > DateTime.Now)
                {
                    db.Otps.Remove(otp);
                    await db.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    db.Otps.Remove(otp);
                    await db.SaveChangesAsync();
                    return BadRequest("Otp Expired");
                }
            }
            return BadRequest("Otp Invalid");
        }

        [HttpPost("sendemail")]
        public async Task<IActionResult> SendEmail(string toemail, string fullname)
        {
            Random rnd = new Random();
            Otp otp = new Otp(rnd.Next(100000, 999999).ToString());
            db.Otps.Add(otp);
            db.SaveChanges();
            //user user = db.Users.Where(x => x.UserName == username).SingleOrDefault();
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration["EmailConfiguration:From"]));
            email.To.Add(MailboxAddress.Parse(toemail));
            email.Subject = "OTP For your login !!!!";
            email.Body = new TextPart(TextFormat.Plain) { Text = "Hello " + fullname + ", Your requested OTP(6 digit) is :" + otp.value };
            using var smtp = new SmtpClient();
            smtp.Connect(configuration["EmailConfiguration:SmtpServer"], Convert.ToInt32(configuration["EmailConfiguration:Port"]), SecureSocketOptions.StartTls);
            //App Password
            smtp.Authenticate(configuration["EmailConfiguration:From"], configuration["EmailConfiguration:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok();

        }

    }
}
