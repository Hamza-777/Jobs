using JobsAPI.Models;

namespace JobsAPI.Repositories.IRepositories
{
    public interface IAuthRepo
    {
        Task<SendResponse> Login(Login user);
        Task<SendResponse> register(user user);
        Task<SendResponse> GetbyUsername(string username);
        Task<SendResponse> UpdatePassword(int userid, user user);
        Task<SendResponse> UpdateUser(int userid,  user user);

    }
}
