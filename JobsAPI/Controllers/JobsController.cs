using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobsRepo _repo;

        public JobsController(IJobsRepo repo)
        {
            _repo = repo;
        }
        // GET: api/Jobs
        [HttpGet]
        public async Task<IActionResult> GetJobs([FromQuery] JobParams jobParams)
        {
            var job= await _repo.GetJobs(jobParams);
            return Ok(job);
        }
                jobsList = jobsList.Where(p => p.title.ToLower().Contains(jobParams.search.ToLower()));
            if (jobParams.categoryId != null || jobParams.cityId != null || jobParams.stateId != null)
            {
                jobsList = FilterJobs(jobParams.categoryId, jobParams.cityId, jobParams.stateId, jobsList);
            }
            if (jobParams.sort != null)
            {
                jobsList = SortBySalary(jobParams.sort, jobsList);
            }
            Console.WriteLine( "count before: ");
            Console.WriteLine(jobsList.Count());
                jobsList = jobsList.Where(p => p.title.ToLower().Contains(jobParams.search.ToLower()));
            jobsList = Pagination(jobParams, jobsList);
            Console.WriteLine("count after: ");
            Console.WriteLine(jobsList.Count());
            if (jobParams.categoryId != null || jobParams.cityId != null || jobParams.stateId != null)
            {
                jobsList = FilterJobs(jobParams.categoryId, jobParams.cityId, jobParams.stateId, jobsList);
            }
            if (jobParams.sort != null)
            {
                jobsList = SortBySalary(jobParams.sort, jobsList);
            }
            Console.WriteLine( "count before: ");
            Console.WriteLine(jobsList.Count());

            jobsList = Pagination(jobParams, jobsList);
            Console.WriteLine("count after: ");
            Console.WriteLine(jobsList.Count());


        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _repo.GetJob(id);
            return Ok(job);

        }

        // Get all cities
        [HttpGet("city")]
        public async Task<IActionResult> GetAllCity()
        {
            var city = await _repo.GetAllCity();
            return Ok(city);
        }

        // get all states
        [HttpGet("state")]
        public async Task<IActionResult> GetAllState()
        {
            var state = await _repo.GetAllState();
            return Ok(state);
        }

        // get all categories
        [HttpGet("category")]
        public async Task<IActionResult> GetAllCategory()
        {
            var category = await _repo.GetAllCategory();
            return Ok(category);
        }


        // PUT: api/Jobs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            return Ok(_repo.PutJob(id, job));
        }

        // POST: api/Jobs
        [HttpPost]
        public async Task<IActionResult> PostJob([FromBody] Job job)
        {
            return Ok(_repo.PostJob(job));
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            return Ok(_repo.DeleteJob(id));
        }

        
    }
}
