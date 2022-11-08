using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobsAPI.Models;

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly userDbContext _context;

        public JobsController(userDbContext context)
        {
            _context = context;
        }
        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs([FromQuery] JobParams jobParams)
        {
            IQueryable<Job> jobsList = _context.Jobs.Include(p => p.category).Include(p => p.state).Include(p => p.city);
            if(jobParams.search != null)
            {
                jobsList = jobsList.Where(p => p.title.ToLower().Contains(jobParams.search.ToLower()));
            }
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

            return await jobsList.ToListAsync();
        }

        private static IQueryable<Job> Pagination(JobParams jobParams, IQueryable<Job> jobsList)
        {

            // Pagination with pagenumber and pagesize
            jobsList = jobsList.Skip((jobParams.pageNumber - 1) * jobParams.PageSize).Take(jobParams.PageSize);
            return jobsList;
        }

        // Filter by city, state, role
        private static IQueryable<Job> FilterJobs(int? catid, int? citid, int? staid, IQueryable<Job> jobsList)
        {
            jobsList = jobsList.Where(p => (!catid.HasValue || p.categoryid == catid) &&
                            (!staid.HasValue || p.stateid == staid) &&
                             (!citid.HasValue || p.cityid == citid));
            return jobsList;
        }

        // sorting by salary 
        private static IQueryable<Job> SortBySalary(string? sort, IQueryable<Job> jobsList)
        {
            
                switch (sort)
                {
                    case "salaryAsc":
                        jobsList = jobsList.OrderBy(p => p.salary_max);
                        break;

                    case "salaryDsc":
                        jobsList = jobsList.OrderByDescending(p => p.salary_max);
                        break;
                }
            

            return jobsList;
        }

        

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs.Include(p => p.category).
                Include(p => p.state).Include(p => p.city).FirstOrDefaultAsync(p => p.Id==id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // Get all cities
        [HttpGet("city")]
        public async Task<ActionResult<IEnumerable<City>>> GetAllCity()
        {
            var city= await _context.Cities.ToListAsync();

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // get all states
        [HttpGet("state")]
        public async Task<ActionResult<IEnumerable<State>>> GetAllState()
        {
            var state = await _context.States.ToListAsync();

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }

        // get all categories
        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            var category = await _context.Categories.ToListAsync();

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }


        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }
            job.city = await _context.Cities.FindAsync(job.cityid);
            job.state = await _context.States.FindAsync(job.stateid);
            job.category = await _context.Categories.FindAsync(job.categoryid);

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob([FromBody] Job job)
        {
            job.city = await _context.Cities.FindAsync(job.cityid);
            job.state = await _context.States.FindAsync(job.stateid);
            job.category = await _context.Categories.FindAsync(job.categoryid);
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
