using JobsAPI.Hashing;
using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace JobsAPI.Repositories
{
    public class AuthRepo: IAuthRepo
    {
        private readonly userDbContext db;

        private IConfiguration configuration;
        private HashMethods hm;
        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AdminRepo));
        public AuthRepo(IConfiguration iConfig, userDbContext _db, HashMethods _hm)
        {
            configuration = iConfig;
            db = _db;
            hm = _hm;
        }
        public async Task<SendResponse> Login(Login user)
        {
            if (user is null)
            {
                _log4net.Error(user+" Invalid client request made");
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Invalid client request");
            }
            user result = null;
            long mobNum;
            if (long.TryParse(user.UserData, out mobNum))
            {

                result = await db.Users.Where(x => x.MobileNumber == mobNum).SingleOrDefaultAsync();
                if (result == null)
                {
                    _log4net.Error(user+ " Unauthorized");
                    return new SendResponse("", StatusCodes.Status401Unauthorized, null, "Unauthorized");
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
                {_log4net.Error(user+ " Unauthorized");
                    return new SendResponse("", StatusCodes.Status401Unauthorized, null, "Unauthorized");
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
                {_log4net.Error(user+ " Unauthorized");
                    return new SendResponse("", StatusCodes.Status401Unauthorized, null, "Unauthorized");
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
                _log4net.Info("Token generated for "+ user);
                return new SendResponse("Token generated", StatusCodes.Status200OK, new AuthenticatedResponse { Token = tokenString }, ""); 
            }
            _log4net.Error("Unauthorized "+ user);
            return new SendResponse("", StatusCodes.Status401Unauthorized, null, "Unauthorized");
        }
        public async Task<SendResponse> register(user user)
        {

            if (user == null)
            {
                _log4net.Warn(user.UserID+ " blank field");
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "All fields are blank");
            }
            if (db.Users.Any(x => x.UserName == user.UserName))
            {
                _log4net.Warn(user.UserID +" Authorized duplicate username");

                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Username is already present");
            }
            else if (db.Users.Any(x => x.EmailId == user.EmailId))
            {
                _log4net.Warn(user.UserID+" Authorized duplicate emailid");

                return new SendResponse("", StatusCodes.Status400BadRequest, null, "EmailID is already present");
            }
            else if (db.Users.Any(x => x.MobileNumber == user.MobileNumber))
            {
                _log4net.Warn(user.UserID+" Authorized duplicate mobilenumber");

                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Mobile Number is already present");
            }
            else
            {
                user.Salt = hm.GenerateSalt();
                user.Password = Convert.ToBase64String(hm.GetHash(user.Password, user.Salt));
                db.Users.Add(user);
                await db.SaveChangesAsync();

                _log4net.Info(user.UserID + " new user Registered");

                return new SendResponse("Registered Successfully ", StatusCodes.Status201Created, user, "");

            }
        }
        public async Task<SendResponse> GetbyUsername(string username)
        {
            var person = await db.Users.Where(x => x.UserName == username).SingleOrDefaultAsync();

            if (person == null)
            {
                _log4net.Error("error finding "+username);
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Username not found");
            }

            person.Password = "...";
            person.Salt = "...";
            _log4net.Info("Get by  " + username + " is revoked");

            return new SendResponse("Found username", StatusCodes.Status200OK, person, "");

        }

        public async Task<SendResponse> UpdatePassword(int userid, user user)
        {
            user.Salt = hm.GenerateSalt();
            user.Password = Convert.ToBase64String(hm.GetHash(user.Password, user.Salt));
            db.Users.Update(user);
            await db.SaveChangesAsync();
            _log4net.Info(userid +"updated password");
            return new SendResponse("Password Updated", StatusCodes.Status201Created, null, "");

        }
        public async Task<SendResponse> UpdateUser(int userid, user user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            _log4net.Info(userid + "updated user");
            return new SendResponse("Updated user successfully", StatusCodes.Status201Created, null, "");
        }
    }
}
