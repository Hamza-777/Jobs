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

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly userDbContext db;
       
        private IConfiguration configuration;
        public AuthController(IConfiguration iConfig, userDbContext _db)
        {
            configuration = iConfig;
            db = _db;
        }
        [HttpPost("login")]
        public async  Task<IActionResult> Login([FromBody] Login user)
        {
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }
            user result=null;
            long mobNum;
            if(long.TryParse(user.UserData,out mobNum))
            {
                 result = await db.Users.Where(x => x.MobileNumber == mobNum && x.Password == user.Password).SingleOrDefaultAsync();
            }
            else if(Regex.IsMatch(user.UserData, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                result = await db.Users.Where(x => x.EmailId ==user.UserData  && x.Password == user.Password).SingleOrDefaultAsync();
            }
            else
            {
                result = await db.Users.Where(x => x.UserName == user.UserData && x.Password == user.Password).SingleOrDefaultAsync();
            }
            if (result!=null)
            {
                var claims = new[]
                {
                    new Claim("FullName",result.FullName),
                    new Claim("Role",result.Role)
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
                return Ok(tokenString/*new AuthenticatedResponse { Token = tokenString }*/);
            }
            return Unauthorized();
        }
        [HttpPost("register")]
        public async Task<ActionResult> register([FromBody] user user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
