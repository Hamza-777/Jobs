using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace JobsAPI.Repositories
{
    public class BlogsRepo: IBlogsRepo
    {
        private readonly userDbContext _context;

        public BlogsRepo(userDbContext context)
        {
            _context = context;
        }

        public async Task<SendResponse> GetBlogs()
        {
            var blogs = await _context.Blogs.ToListAsync();
            if (blogs.Count() > 0)
            {
                return new SendResponse("Blogs Found", StatusCodes.Status200OK, blogs, "");

            }
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find Blogs");
        }
        public async Task<SendResponse> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any blog");
            }
            return new SendResponse("Blog Found", StatusCodes.Status200OK, blog, "");
        }
        public async Task<SendResponse> PutBlog(int id, Blog blog)
        {
            if (id != blog.BlogId)
            {
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Bad Request of id");

            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new SendResponse("Edited Blog Successfully", StatusCodes.Status201Created, null, "");

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
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
                return new SendResponse("", StatusCodes.Status406NotAcceptable, null, "Blog cannot be posted");
            }
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return new SendResponse("Posted Blog", StatusCodes.Status201Created, null, "");
        }
        public async Task<SendResponse> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any blog of id");
            }
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return new SendResponse("Deleted blog successfully", StatusCodes.Status200OK, null, "");
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }

    }
}
