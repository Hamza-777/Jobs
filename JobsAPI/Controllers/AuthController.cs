using JobsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using JobsAPI.Hashing;
using JobsAPI.Repositories.IRepositories;

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _repo;
        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        public AuthController(IAuthRepo repo)
        {
            _repo = repo;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login user)
        {
            _log4net.Info("Login user of auth controller revoked " + user.UserData);
            return Ok(await _repo.Login(user));
        }
        [HttpPost("register")]
        public async Task<ActionResult> register([FromBody] user user)
        {
            _log4net.Info("Register user of auth controller revoked " + user.UserID);
            return Ok(await _repo.register(user));
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<user>> GetbyUsername(string username)
        {
            _log4net.Info("Get username "+username+" of auth controller revoked ");
            return Ok(await _repo.GetbyUsername(username));
        }

        [HttpPut("updatepassword/{userid}")]
        public async Task<ActionResult> UpdatePassword(int userid, [FromBody] user user)
        {
            _log4net.Info("Update password of  " + userid + " of auth controller revoked ");
            return Ok(await _repo.UpdatePassword(userid, user));
        }
        [HttpPut("updateuser/{userid}")]
        public async Task<ActionResult> UpdateUser(int userid, [FromBody] user user)
        {
            _log4net.Info("Update User of  " + userid + " of auth controller revoked ");

            return Ok(await _repo.UpdateUser(userid, user));
        }
    }
}
