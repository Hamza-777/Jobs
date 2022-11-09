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

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOtpRepo _repo;

        private IConfiguration configuration;
        public OtpController(IOtpRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("clearotp")]
        public async Task<IActionResult> ClearOTP()
        {
            return Ok(await _repo.ClearOTP());
        }
        [HttpGet("checkotp/{value}")]
        public async Task<IActionResult> CheckOTP(string value)
        {
            return Ok(await _repo.CheckOTP(value));
        }

        [HttpPost("sendemail/{toemail}/{fullname}")]
        public async Task<IActionResult> SendEmail(string toemail, string fullname)
        {
            return Ok(await _repo.SendEmail(toemail, fullname));
        }

        

    }
}
