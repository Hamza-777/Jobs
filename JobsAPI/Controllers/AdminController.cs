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
        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AdminController));

        public AdminController(IAdminRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            _log4net.Info("Get users of admin controller invoked");
            return Ok(await _repo.GetUsers());
        }

        [HttpGet("getuserbyid/{id}")]
        public async Task<ActionResult<user>> GetUserById(int id)
        {
            _log4net.Info("Get users "+ id+" of admin controller invoked");

            return Ok(await _repo.GetUserById(id));
        }

        [HttpDelete("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _log4net.Info("Delete users " + id + " of admin controller invoked");
            return Ok(await _repo.DeleteUser(id));
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] user user)
        {
            _log4net.Info("Register Admin " + user.UserID+ " of admin controller invoked");
            return Ok(await _repo.RegisterAdmin(user));
        }
    }
}
