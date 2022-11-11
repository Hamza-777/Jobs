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
        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AdminRepo));

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
                _log4net.Info("Get Users is invoked");
                return new SendResponse("Users Found", StatusCodes.Status200OK, users, "");
            }
            _log4net.Error("Error finding users");
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any user");
        }
        public async Task<SendResponse> GetUserById(int id)
        {
            var person = await db.Users.FindAsync(id);

            if (person == null)
            {
                _log4net.Info("Get user by " + id + " is invoked");
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any user");
            }
            _log4net.Error("Error finding user "+ id);
            return new SendResponse("User Found", StatusCodes.Status200OK, person, "");
        }

        public async Task<SendResponse> DeleteUser(int id)
        {
            var person = await db.Users.FindAsync(id);
            if (person == null)
            {
                _log4net.Error("Error deleting user "+ id);
                return new SendResponse("", StatusCodes.Status404NotFound, null, "User Not Found");
            }
            db.Users.Remove(person);
            await db.SaveChangesAsync();

            _log4net.Info("Delete user by " + id + " is invoked");
            return new SendResponse("Deleted user successfully", StatusCodes.Status200OK, person, "");
        }
        public async Task<SendResponse> RegisterAdmin(user user)
        {
            if (user == null)
            {
                _log4net.Warn(user.UserID+ " Register admin fields blank");
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "All fields are blank");
            }
            if (db.Users.Any(x => x.UserName == user.UserName))
            {
                _log4net.Warn(user.UserID+ " Register admin username duplicate");

                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Username is already present");
            }
            else if (db.Users.Any(x => x.EmailId == user.EmailId))
            {
                _log4net.Warn(user.UserID+ " Register admin email id duplicate");

                return new SendResponse("", StatusCodes.Status400BadRequest, null, "EmailID is already present");
            }
            else if (db.Users.Any(x => x.MobileNumber == user.MobileNumber))
            {
                _log4net.Warn(user.UserID + " Register admin mobilenumber duplicate");

                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Mobile Number is already present");
            }
            else
            {
                user.Salt = hm.GenerateSalt();
                user.Password = Convert.ToBase64String(hm.GetHash(user.Password, user.Salt));
                user.Role = "Admin";
                db.Users.Add(user);
                await db.SaveChangesAsync();

                _log4net.Info(user.UserID + " Registered");
                return new SendResponse("Registered Successfully ", StatusCodes.Status200OK, user, "");

            }
        }
    }
}
