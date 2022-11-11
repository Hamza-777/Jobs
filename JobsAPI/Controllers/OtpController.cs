using JobsAPI.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using NuGet.Protocol;
using JobsAPI.Hashing;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using JobsAPI.Repositories.IRepositories;

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOtpRepo _repo;

        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(JobsController));

        public OtpController(IOtpRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("clearotp")]
        public async Task<IActionResult> ClearOTP()
        {
            _log4net.Info("clear otp function revoked");
            return Ok(await _repo.ClearOTP());
        }
        [HttpGet("checkotp/{value}")]
        public async Task<IActionResult> CheckOTP(string value)
        {
            _log4net.Info("Check otp function revoked");

            return Ok(await _repo.CheckOTP(value));
        }

        [HttpPost("sendemail/{toemail}/{fullname}")]
        public async Task<IActionResult> SendEmail(string toemail, string fullname)
        {
            _log4net.Info("Send otpto mail function revoked");

            return Ok(await _repo.SendEmail(toemail, fullname));
        }

        

    }
}
