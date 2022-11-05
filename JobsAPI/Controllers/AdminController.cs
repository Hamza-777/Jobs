using JobsAPI.Hashing;
using JobsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly userDbContext db;
        public AdminController( userDbContext _db)
        {
            db = _db;

        }

        [HttpGet("getusers")]
        public async Task<ActionResult<IEnumerable<user>>> GetUsers()
        {
            return await db.Users.ToListAsync();
        }

        [HttpGet("getuserbyid/{id}")]
        public async Task<ActionResult<user>> GetUserById(int id)
        {
            var person = await db.Users.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;

        }

        [HttpDelete("deleteuser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var person = await db.Users.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            db.Users.Remove(person);
            await db.SaveChangesAsync();
            return Ok();

        }
    }
}
