using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace JobsAPI.Repositories
{
    public class BlogsRepo: IBlogsRepo
    {
        private readonly userDbContext _context;
        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BlogsRepo));

        public BlogsRepo(userDbContext context)
        {
            _context = context;
        }

       
        public async Task<SendResponse> GetBlogs()
        {
            var blogs = await _context.Blogs.ToListAsync();
            if (blogs.Count() > 0)
            {
                _log4net.Info("Get Blogs revoked");
                return new SendResponse("Blogs Found", StatusCodes.Status200OK, blogs, "");

            }
            _log4net.Error("Error getting the blogs");
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find Blogs");
        }
        public async Task<SendResponse> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                _log4net.Error("Error getting blog "+id);

                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any blog");
            }
            _log4net.Info("Get Blog "+id+" revoked");
            return new SendResponse("Blog Found", StatusCodes.Status200OK, blog, "");
        }
        public async Task<SendResponse> PutBlog(int id, Blog blog)
        {
            if (id != blog.BlogId)
            {
                _log4net.Error("bad request made on" + id);
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Bad Request of id");
            }
            _context.Entry(blog).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _log4net.Info("Edit Blog revoked on " + id);
                return new SendResponse("Edited Blog Successfully", StatusCodes.Status201Created, null, "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    _log4net.Error("Error finding blog of " + id);
                    return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any Blog of id");
                }
                else
                {
                    throw;
                }
            }
            return new SendResponse("", StatusCodes.Status204NoContent, null, "Blog not found");
        }

        public async Task<SendResponse> PostBlog(Blog blog)
        {
            if (blog == null)
            {
                _log4net.Error("Error posting blog");
                return new SendResponse("", StatusCodes.Status406NotAcceptable, null, "Blog cannot be posted");
            }
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            _log4net.Info("Post blog revoke" + blog.BlogId);
            return new SendResponse("Posted Blog", StatusCodes.Status201Created, blog, "");
        }
        public async Task<SendResponse> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                _log4net.Error("Error deleting blog " + id);
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any blog of id");
            }
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
<<<<<<< HEAD
            _log4net.Info("Delete blog revoked" + id);
            return new SendResponse("Deleted blog successfully", StatusCodes.Status200OK, null, "");
=======
            return new SendResponse("Deleted blog successfully", StatusCodes.Status200OK, blog, "");
>>>>>>> main
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }

    }
}
