using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobsAPI.Models;
using System.Xml.Linq;

namespace JobsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly userDbContext _context;

        public CoursesController(userDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
           
            return await _context.Courses.ToListAsync();
        }



        // GET: api/Courses/5

        //[Route("{id:int}")]
        //[HttpGet("example/{param1}/{param2:Guid}")]

        [HttpGet("{id:int}")]
        
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        [HttpGet("name")]
        //[HttpGet("{name:string}")]
        public async Task<ActionResult<Course>> GetCourseByName(string name)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.CourseName == name);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        [HttpGet("CategoryName")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByCategory([FromQuery]string CategoryName)
        {

            
            //try
            //{
                return await _context.Courses.Where(e => e.CourseCategory == CategoryName).ToListAsync();
            //}
            //catch()
            //{
            //    return NotFound();
            //}
            
        }


        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }


        
        
        
        
        
        
        
        
        
        
        //[HttpGet("{name}")]
        //[HttpGet("{name:string}")]
        // async Task<ActionResult<Course>> GetCourseByName(string name)
        //{
        //    var course = await _context.Courses.FirstOrDefaultAsync(e => e.CourseName == name);

        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    return course;
        //}

        //[HttpGet("{CategoryName:alpha}")]
        //public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByCategory(string CategoryName)
        //{


        //    //try
        //    //{
        //    return await _context.Courses.Where(e => e.CourseCategory == CategoryName).ToListAsync();
        //    //}
        //    //catch()
        //    //{
        //    //    return NotFound();
        //    //}

        //}


    }
}
