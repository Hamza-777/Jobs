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
        private IConfiguration configuration;
        private HashMethods hm;
        public AdminController(IConfiguration iConfig, userDbContext _db, HashMethods _hm)
        {
            configuration = iConfig;
            db = _db;
            hm = _hm;

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
        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult> RegisterAdmin([FromBody] user user)
        {
            if (user == null)
            {
                return BadRequest("All fields are blank");
            }
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.UserName == user.UserName))
                {
                    return BadRequest("Username is already present");
                }
                else if (db.Users.Any(x => x.EmailId == user.EmailId))
                {
                    return BadRequest("EmailID is already present");
                }
                else if (db.Users.Any(x => x.MobileNumber == user.MobileNumber))
                {
                    return BadRequest("Mobile Number is already present");
                }
                else
                {
                    user.Salt = hm.GenerateSalt();
                    user.Password = Convert.ToBase64String(hm.GetHash(user.Password, user.Salt));
                    user.Role = "Admin";
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                }
            }
            return Ok();
        }
    }
}
