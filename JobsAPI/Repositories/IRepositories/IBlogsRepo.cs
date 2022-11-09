using JobsAPI.Models;

namespace JobsAPI.Repositories.IRepositories
{
    public interface IBlogsRepo
    {
        Task<SendResponse> GetBlogs();
        Task<SendResponse> GetBlog(int id);
        Task<SendResponse> PutBlog(int id, Blog blog);
        Task<SendResponse> PostBlog(Blog blog);
        Task<SendResponse> DeleteBlog(int id);

    }
}
