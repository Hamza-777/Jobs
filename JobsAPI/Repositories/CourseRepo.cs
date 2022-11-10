using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsAPI.Repositories
{
    public class CourseRepo : ICourseRepo
    {
        private readonly userDbContext _context;
        public CourseRepo(userDbContext context)
        {
            _context = context;
        }
        public async Task<SendResponse> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            if (courses.Count() > 0)
            {
                return new SendResponse("course Found", StatusCodes.Status200OK, courses, "");

            }
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find Courses");
        }

        public async Task<SendResponse> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any course");
            }
            return new SendResponse("course Found", StatusCodes.Status200OK, course, "");
        }

        public async Task<SendResponse> GetCourseByName(string name)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.CourseName == name);

            if (course == null)
            {
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any course of this name");
            }
            return new SendResponse("Course of given name is found", StatusCodes.Status200OK, course, "");
        }
        public async Task<SendResponse> GetCoursesByCategory([FromQuery] string CategoryName)
        {
            var category = await _context.Courses.Where(e => e.CourseCategory == CategoryName).ToListAsync();
            if (category == null)
            {
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any course of this category");
            }
            return new SendResponse("Course of given category is found", StatusCodes.Status200OK, category, "");
        }
        public async Task<SendResponse> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Bad Request of id");
            }
            _context.Entry(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, null, "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any course");
                }
                else
                {
                    throw;
                }
            }
            return new SendResponse("", StatusCodes.Status204NoContent, null, "course not found");
        }
        public async Task<SendResponse> PostCourse(Course course)
        {
            if (course == null)
            {
                return new SendResponse("", StatusCodes.Status406NotAcceptable, null, "Course cannot be posted");
            }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return new SendResponse("Posted course", StatusCodes.Status201Created, null, "");

        }

        public async Task<SendResponse> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any course");
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return new SendResponse("Deleted course successfully", StatusCodes.Status200OK, course, "");
        }
        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
