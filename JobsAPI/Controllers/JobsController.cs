using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobsRepo _repo;
        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(JobsController));
        public JobsController(IJobsRepo repo)
        {
            _repo = repo;
        }
        // GET: api/Jobs
        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            _log4net.Info("Get all jobs of jobs controller got revoked");
            var job = await _repo.GetJobs();
            return Ok(job);
        }
        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            _log4net.Info("Get jobs by id " + id + " of jobs controller got revoked");
            var job = await _repo.GetJob(id);
            return Ok(job);

        }

        // Get all cities
        [HttpGet("city")]
        public async Task<IActionResult> GetAllCity()
        {
            _log4net.Info("Get all cities of jobs controller got revoked");
            var city = await _repo.GetAllCity();
            return Ok(city);
        }

        // get all states
        [HttpGet("state")]
        public async Task<IActionResult> GetAllState()
        {
            _log4net.Info("Get all states of jobs controller got revoked");
            var state = await _repo.GetAllState();
            return Ok(state);
        }

        // get all categories
        [HttpGet("category")]
        public async Task<IActionResult> GetAllCategory()
        {
            _log4net.Info("Get all categories of jobs controller got revoked");
            var category = await _repo.GetAllCategory();
            return Ok(category);
        }


        // PUT: api/Jobs/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            _log4net.Info("Put jobs of jobs controller got revoked of id "+ id);

            return Ok(await _repo.PutJob(id, job));
        }

        // POST: api/Jobs
        [HttpPost]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> PostJob([FromBody] Job job)
        {
            _log4net.Info("Post jobs of jobs controller got revoked of id " + job.Id);
            return Ok( await _repo.PostJob(job));
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Recruiter")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            _log4net.Info("Delete jobs of jobs controller got revoked of id " + id);

            return Ok(await _repo.DeleteJob(id));
        }

        
    }
}
