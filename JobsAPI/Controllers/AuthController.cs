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


namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly userDbContext db;

        private IConfiguration configuration;
        private HashMethods hm;
        public AuthController(IConfiguration iConfig, userDbContext _db, HashMethods _hm)
        {
            configuration = iConfig;
            db = _db;
            hm = _hm;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }
            user result = null;
            long mobNum;
            if (long.TryParse(user.UserData, out mobNum))
            {

                result = await db.Users.Where(x => x.MobileNumber == mobNum).SingleOrDefaultAsync();
                if (result == null)
                {
                    return Unauthorized();
                }
                if (!hm.CompareHashedPasswords(user.Password, result.Password, result.Salt))
                {
                    result = null;
                }
            }
            else if (Regex.IsMatch(user.UserData, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                result = await db.Users.Where(x => x.EmailId == user.UserData).SingleOrDefaultAsync();
                if (result == null)
                {
                    return Unauthorized();
                }
                if (!hm.CompareHashedPasswords(user.Password, result.Password, result.Salt))
                {
                    result = null;
                }
            }
            else
            {
                result = await db.Users.Where(x => x.UserName == user.UserData).SingleOrDefaultAsync();
                if (result == null)
                {
                    return Unauthorized();
                }
                if (!hm.CompareHashedPasswords(user.Password, result.Password, result.Salt))
                {
                    result = null;
                }
            }
            if (result != null)
            {
                var claims = new[]
                {
                    new Claim("UserID",result.UserID.ToString()?? ""),
                    new Claim("FullName",result.FullName?? ""),
                    new Claim("UserName",result.UserName?? ""),
                    new Claim("EmailId",result.EmailId?? ""),
                    new Claim("MobileNumber",result.MobileNumber.ToString()?? ""),
                    new Claim(ClaimTypes.Role,result.Role?? ""),
                    new Claim("Role",result.Role?? "")

                };
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new AuthenticatedResponse { Token = tokenString });
            }
            return Unauthorized();
        }
        [HttpPost("register")]
        public async Task<ActionResult> register([FromBody] user user)
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
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                }
            }
            return Ok();
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<user>> GetbyUsername(string username)
        {
            var person = await db.Users.Where(x => x.UserName == username).SingleOrDefaultAsync();

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        [HttpPut("updatepassword/{userid}")]
        public async Task<ActionResult> UpdatePassword(int userid, [FromBody] user user)
        {
            user.Salt = hm.GenerateSalt();
            user.Password = Convert.ToBase64String(hm.GetHash(user.Password, user.Salt));
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("updateuser/{userid}")]
        public async Task<ActionResult> UpdateUser(int userid, [FromBody] user user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
