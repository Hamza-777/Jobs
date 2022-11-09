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
        public AuthController(IAuthRepo repo)
        {
            _repo = repo;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login user)
        {
            return Ok(await _repo.Login(user));
        }
        [HttpPost("register")]
        public async Task<ActionResult> register([FromBody] user user)
        {
            return Ok(await _repo.register(user));
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<user>> GetbyUsername(string username)
        {
            return Ok(await _repo.GetbyUsername(username));
        }

        [HttpPut("updatepassword/{userid}")]
        public async Task<ActionResult> UpdatePassword(int userid, [FromBody] user user)
        {
            return Ok(await _repo.UpdatePassword(userid, user));
        }
        [HttpPut("updateuser/{userid}")]
        public async Task<ActionResult> UpdateUser(int userid, [FromBody] user user)
        {
            return Ok(await _repo.UpdateUser(userid, user));
        }
    }
}
