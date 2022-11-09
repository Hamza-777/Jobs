using JobsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobsAPI.Repositories.IRepositories
{
    public interface IJobsRepo
    {
        Task<SendResponse> GetJobs(JobParams jobParams);
        Task<SendResponse> GetJob(int id);
        Task<SendResponse> GetAllCity();
        Task<SendResponse> GetAllState();
        Task<SendResponse> GetAllCategory();
        Task<SendResponse> PutJob(int id, Job job);
        Task<SendResponse> PostJob(Job job);

        Task<SendResponse> DeleteJob(int id);





    }
}
