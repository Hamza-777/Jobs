using JobsAPI.Hashing;
using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo _repo;
        public AdminController(IAdminRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _repo.GetUsers());
        }

        [HttpGet("getuserbyid/{id}")]
        public async Task<ActionResult<user>> GetUserById(int id)
        {
            return Ok(await _repo.GetUserById(id));
        }

        [HttpDelete("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _repo.DeleteUser(id));
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] user user)
        {
            return Ok(await _repo.RegisterAdmin(user));
        }
    }
}
