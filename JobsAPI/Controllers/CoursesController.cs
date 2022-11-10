using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobsAPI.Models;
using System.Xml.Linq;
using JobsAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepo _repo;

        public CoursesController(ICourseRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _repo.GetCourses());
        }

        // GET: api/Courses/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            return Ok(await _repo.GetCourse(id));
        }

        [HttpGet("name")]
        public async Task<ActionResult<Course>> GetCourseByName(string name)
        {
            return Ok(await _repo.GetCourseByName(name));
        }

        [HttpGet("CategoryName")]
        public async Task<IActionResult> GetCoursesByCategory([FromQuery]string CategoryName)
        {
            return Ok(await _repo.GetCoursesByCategory(CategoryName)); 
            
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            return Ok(await _repo.PutCourse(id, course));
        }

        // POST: api/Courses
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostCourse(Course course)
        {
            return Ok(await _repo.PostCourse(course));
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            return Ok(await _repo.DeleteCourse(id));
        }
    }
}
