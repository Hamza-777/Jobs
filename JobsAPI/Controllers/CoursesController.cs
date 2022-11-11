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
        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CoursesController));
        public CoursesController(ICourseRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            _log4net.Info("Get courses of course controller revoked");
            return Ok(await _repo.GetCourses());
        }

        // GET: api/Courses/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            _log4net.Info("Get Course by id " + id + " of course controller revoked");

            return Ok(await _repo.GetCourse(id));
        }

        [HttpGet("name")]
        public async Task<ActionResult<Course>> GetCourseByName(string name)
        {
            _log4net.Info("Get Course by name " + name + " of course controller revoked");

            return Ok(await _repo.GetCourseByName(name));
        }

        [HttpGet("CategoryName")]
        public async Task<IActionResult> GetCoursesByCategory([FromQuery]string CategoryName)
        {
            _log4net.Info("Get Course by category " + CategoryName + " of course controller revoked");

            return Ok(await _repo.GetCoursesByCategory(CategoryName)); 
            
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            _log4net.Info("Put Course by id " + id + " of course controller revoked");
            return Ok(await _repo.PutCourse(id, course));
        }

        // POST: api/Courses
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostCourse(Course course)
        {
            _log4net.Info("Post Course " + course.CourseId + " of course controller revoked");
            return Ok(await _repo.PostCourse(course));
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            _log4net.Info("Delete Course of " + id + " of course controller revoked");
            return Ok(await _repo.DeleteCourse(id));
        }
    }
}
