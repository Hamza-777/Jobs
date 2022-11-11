using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsAPI.Repositories
{
    public class JobsRepo : IJobsRepo
    {
        private readonly userDbContext _context;
        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CourseRepo));
        public JobsRepo(userDbContext context)
        {
            _context = context;
        }
        public  async Task<SendResponse> GetJobs()
        {
            var jobsList =  await _context.Jobs.Include(p => p.category).Include(p => p.state).Include(p => p.city).ToListAsync();

            if (jobsList.Count() > 0)
            {
                _log4net.Info("Get jobs revoked");
                return new SendResponse("jobs Found", StatusCodes.Status200OK,jobsList, "");
            }
            _log4net.Error("Error getting jobs");
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Jobs");
        }
        public async Task<SendResponse> GetJob(int id)
        {
            var job = await _context.Jobs.Include(p => p.category).
                Include(p => p.state).Include(p => p.city).FirstOrDefaultAsync(p => p.Id == id);
            if (job == null)
            {
                _log4net.Error("Error getting job  of id"+ id);
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Job");
            }
            _log4net.Info("get job by id " + id + "revoked");
            return new SendResponse("Job Found", StatusCodes.Status200OK, job, "");
        }

        // Get all cities
        public async Task<SendResponse> GetAllCity()
        {
            var city = await _context.Cities.ToListAsync();
            if (city.Count() > 0)
            {
                _log4net.Info("get all city revoked");
                return new SendResponse("Cities Found", StatusCodes.Status200OK, city, "");
            }
            _log4net.Error("Error getting cities");
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find Cities");
        }

        // get all states
        public async Task<SendResponse> GetAllState()
        {
            var state = await _context.States.ToListAsync();
            if (state.Count() > 0)
            {
                _log4net.Info("get all states revoked");
                return new SendResponse("States Found", StatusCodes.Status200OK, state, "");
            }
            _log4net.Error("Error getting states");
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any States");
        }

        // get all categories
        public async Task<SendResponse> GetAllCategory()
        {
            var category = await _context.Categories.ToListAsync();
            if (category.Count() > 0)
            {
                _log4net.Info("get all categories revoked");
                return new SendResponse("Categories Found", StatusCodes.Status200OK, category, "");
            }
            _log4net.Error("Error getting categories");
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Categories");
        }

        public async Task<SendResponse> PutJob(int id, Job job)
        {
            if (id != job.Id)
            {
                _log4net.Error("Error editing job " + id);
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Bad Request of id");
            }
            job.city = await _context.Cities.FindAsync(job.cityid);
            job.state = await _context.States.FindAsync(job.stateid);
            job.category = await _context.Categories.FindAsync(job.categoryid);

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                _log4net.Info("Edit job by id" + id + " revoked");

                return new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, job, "");

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    _log4net.Error("error finding job for edit");
                    return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Jobs");
                }
                else
                {
                    throw;
                }
            }
            _log4net.Error("error finding job for edit");
            return new SendResponse("", StatusCodes.Status204NoContent, null, "Job not found");
        }

        public async Task<SendResponse> PostJob([FromBody] Job job)
        {
            job.city = await _context.Cities.FindAsync(job.cityid);
            job.state = await _context.States.FindAsync(job.stateid);
            job.category = await _context.Categories.FindAsync(job.categoryid);
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            _log4net.Info("post course revoked"+job.Id);
            return new SendResponse("Posted Job", StatusCodes.Status201Created, null, "");
        }
        public async Task<SendResponse> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                _log4net.Error("Error deleting job" + id);
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Job");

            }
            
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            _log4net.Info("Deleted job revoked" + id);

            return new SendResponse("Deleted Job successfully", StatusCodes.Status200OK, job, "");

        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
