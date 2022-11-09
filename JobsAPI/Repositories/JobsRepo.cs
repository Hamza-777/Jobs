using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsAPI.Repositories
{
    public class JobsRepo : IJobsRepo
    {

        private readonly userDbContext _context;

        public JobsRepo(userDbContext context)
        {
            _context = context;
        }
        public  async Task<SendResponse> GetJobs([FromQuery] JobParams jobParams)
        {
            IQueryable<Job> jobsList =  _context.Jobs.Include(p => p.category).Include(p => p.state).Include(p => p.city);
            
            if (jobParams.search != null)
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

            //jobsList = Pagination(jobParams, jobsList);
            if (jobsList.Count() > 0)
            {
                return new SendResponse("jobs Found", StatusCodes.Status200OK,jobsList, "");
            }
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Jobs");
            
        }

        //Pagination
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

        public async Task<SendResponse> GetJob(int id)
        {
            var job = await _context.Jobs.Include(p => p.category).
                Include(p => p.state).Include(p => p.city).FirstOrDefaultAsync(p => p.Id == id);
            if (job == null)
            {
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Job");
            }
            return new SendResponse("Job Found", StatusCodes.Status200OK, job, "");
        }

        // Get all cities
        public async Task<SendResponse> GetAllCity()
        {
            var city = await _context.Cities.ToListAsync();
            if (city.Count() > 0)
            {
                return new SendResponse("Cities Found", StatusCodes.Status200OK, city, "");

            }
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find Cities");
        }

        // get all states
        public async Task<SendResponse> GetAllState()
        {
            var state = await _context.States.ToListAsync();
            if (state.Count() > 0)
            {
                return new SendResponse("States Found", StatusCodes.Status200OK, state, "");

            }
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any States");
        }

        // get all categories
        public async Task<SendResponse> GetAllCategory()
        {
            var category = await _context.Categories.ToListAsync();
            if (category.Count() > 0)
            {
                return new SendResponse("Categories Found", StatusCodes.Status200OK, category, "");

            }
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Categories");
        }

        public async Task<SendResponse> PutJob(int id, Job job)
        {
            if (id != job.Id)
            {
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Bad Request of id");
            }
            job.city = await _context.Cities.FindAsync(job.cityid);
            job.state = await _context.States.FindAsync(job.stateid);
            job.category = await _context.Categories.FindAsync(job.categoryid);

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, null, "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Jobs");
                }
                else
                {
                    throw;
                }
            }

            return new SendResponse("", StatusCodes.Status204NoContent, null, "Job not found");

        }

        public async Task<SendResponse> PostJob([FromBody] Job job)
        {
            job.city = await _context.Cities.FindAsync(job.cityid);
            job.state = await _context.States.FindAsync(job.stateid);
            job.category = await _context.Categories.FindAsync(job.categoryid);
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return new SendResponse("Posted Job", StatusCodes.Status201Created, null, "");
        }
        public async Task<SendResponse> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Job");

            }
            
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return new SendResponse("Deleted Job successfully", StatusCodes.Status200OK, null, "");
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
