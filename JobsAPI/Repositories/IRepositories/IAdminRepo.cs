using JobsAPI.Models;

namespace JobsAPI.Repositories.IRepositories
{
    public interface IAdminRepo
    {
        Task<SendResponse> GetUsers();
        Task<SendResponse> GetUserById(int id);
        Task<SendResponse> DeleteUser(int id);
        Task<SendResponse> RegisterAdmin(user user);

    }
}
