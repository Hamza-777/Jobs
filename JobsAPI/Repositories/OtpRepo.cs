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
using JobsAPI.Repositories.IRepositories;

namespace JobsAPI.Repositories
{
    public class OtpRepo:IOtpRepo
    {
        private readonly userDbContext db;

        private IConfiguration configuration;
        public OtpRepo(IConfiguration iConfig, userDbContext _db)
        {
            configuration = iConfig;
            db = _db;
        }
        public async Task<SendResponse> ClearOTP()
        {
            List<Otp> invalidotps = db.Otps.Where(x => x.Timestamp.AddMinutes(2) < DateTime.Now).ToList();
            db.Otps.RemoveRange(invalidotps);
            await db.SaveChangesAsync();
            return new SendResponse("Otp Cleared successfully", StatusCodes.Status205ResetContent, invalidotps.ToJson(), "");
        }
        public async Task<SendResponse> CheckOTP(string value)
        {
            Otp otp = await db.Otps.Where(x => x.value == value).SingleOrDefaultAsync();
            await ClearOTP();
            if (otp != null)
            {
                if (otp.Timestamp.AddMinutes(2) > DateTime.Now)
                {
                    db.Otps.Remove(otp);
                    await db.SaveChangesAsync();
                    return new SendResponse("Otp removed successfully", StatusCodes.Status205ResetContent, null, "");
                }
                else
                {
                    db.Otps.Remove(otp);
                    await db.SaveChangesAsync();
                    return new SendResponse("", StatusCodes.Status400BadRequest, null, "Otp Expired");
                }
            }
            return new SendResponse("", StatusCodes.Status400BadRequest, null, "Otp Invalid");
        }
        public async Task<SendResponse> SendEmail(string toemail, string fullname)
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
            return new SendResponse("Otp sent Successfully to the email", StatusCodes.Status200OK, null, "");

        }
    }
}
