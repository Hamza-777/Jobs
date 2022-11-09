using JobsAPI.Hashing;
using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.ModelBinding;

namespace JobsAPI.Repositories
{
    public class AdminRepo : IAdminRepo
    {
        private readonly userDbContext db;
        private IConfiguration configuration;
        private HashMethods hm;
        public AdminRepo(IConfiguration iConfig, userDbContext _db, HashMethods _hm)
        {
            configuration = iConfig;
            db = _db;
            hm = _hm;

        }
        public async Task<SendResponse> GetUsers()
        {
            var users = await db.Users.ToListAsync();
            if (users.Count() > 0)
            {
                return new SendResponse("Users Found", StatusCodes.Status200OK, users, "");
            }
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any user");
        }
        public async Task<SendResponse> GetUserById(int id)
        {
            var person = await db.Users.FindAsync(id);

            if (person == null)
            {
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any user");
            }
            return new SendResponse("User Found", StatusCodes.Status200OK, person, "");
        }

        public async Task<SendResponse> DeleteUser(int id)
        {
            var person = await db.Users.FindAsync(id);
            if (person == null)
            {
                return new SendResponse("", StatusCodes.Status404NotFound, null, "User Not Found");
            }
            db.Users.Remove(person);
            await db.SaveChangesAsync();
            return new SendResponse("Deleted user successfully", StatusCodes.Status200OK, null, "");
        }
        public async Task<SendResponse> RegisterAdmin(user user)
        {
            if (user == null)
            {
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "All fields are blank");
            }
            if (db.Users.Any(x => x.UserName == user.UserName))
            {
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Username is already present");
            }
            else if (db.Users.Any(x => x.EmailId == user.EmailId))
            {
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "EmailID is already present");
            }
            else if (db.Users.Any(x => x.MobileNumber == user.MobileNumber))
            {
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Mobile Number is already present");
            }
            else
            {
                user.Salt = hm.GenerateSalt();
                user.Password = Convert.ToBase64String(hm.GetHash(user.Password, user.Salt));
                user.Role = "Admin";
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return new SendResponse("Registered Successfully ", StatusCodes.Status200OK, null, "");

            }
        }
    }
}
