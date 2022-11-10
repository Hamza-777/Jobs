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
    public class BlogsController : ControllerBase
    {
        private readonly IBlogsRepo _repo;

        public BlogsController(IBlogsRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            return Ok(await _repo.GetBlogs());
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            return Ok(await _repo.GetBlog(id));
        }

        // PUT: api/Blogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            return Ok(await _repo.PutBlog(id, blog));
        }

        // POST: api/Blogs
        [HttpPost]
        public async Task<IActionResult> PostBlog(Blog blog)
        {
            return Ok(await _repo.PostBlog(blog));
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            return Ok(await _repo.DeleteBlog(id));

        }
    }
}
