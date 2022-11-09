using JobsAPI.Models;

namespace JobsAPI.Repositories.IRepositories
{
    public interface ICourseRepo
    {
        Task<SendResponse> GetCourses();
        Task<SendResponse> GetCourse(int id);
        Task<SendResponse> GetCourseByName(string name);

        Task<SendResponse> GetCoursesByCategory(string CategoryName);
        Task<SendResponse> PutCourse(int id, Course course);
        Task<SendResponse> PostCourse(Course course);
        Task<SendResponse> DeleteCourse(int id);

    }
}
