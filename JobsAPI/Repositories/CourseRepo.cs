using JobsAPI.Models;
using JobsAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsAPI.Repositories
{
    public class CourseRepo : ICourseRepo
    {
        private readonly userDbContext _context;
        public static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CourseRepo));

        public CourseRepo(userDbContext context)
        {
            _context = context;
        }
        public async Task<SendResponse> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            if (courses.Count() > 0)
            {

                _log4net.Info("Get courses revoked");

                return new SendResponse("course Found", StatusCodes.Status200OK, courses, "");


            }
            _log4net.Error("Error getting courses");
            return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find Courses");
        }

        public async Task<SendResponse> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                _log4net.Error("Error getting course of " + id);
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any course");
            }
            _log4net.Info("Get course by id " + id + " revoked");
            return new SendResponse("course Found", StatusCodes.Status200OK, course, "");
        }

        public async Task<SendResponse> GetCourseByName(string name)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(e => e.CourseName == name);
            if (course == null)
            {
                _log4net.Error("Error finding course of name " + name);
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any course of this name");
            }
            _log4net.Info("Get by name" + name + "got revoked");
            return new SendResponse("Course of given name is found", StatusCodes.Status200OK, course, "");
        }
        public async Task<SendResponse> GetCoursesByCategory([FromQuery] string CategoryName)
        {
            var category = await _context.Courses.Where(e => e.CourseCategory == CategoryName).ToListAsync();
            if (category == null)
            {
                _log4net.Error("Error finding course of category " + CategoryName);
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any course of this category");
            }
            _log4net.Info("Get by category" + CategoryName + "got revoked");
            return new SendResponse("Course of given category is found", StatusCodes.Status200OK, category, "");
        }

        public async Task<SendResponse> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return new SendResponse("", StatusCodes.Status400BadRequest, null, "Bad Request of id");
                _log4net.Error("Error editing course of"+ id);

            }
            _context.Entry(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _log4net.Info("Edit course revoked " + id);
                return new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, null, "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    _log4net.Error("Error finding course in edit of id " + id);

                    return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any course");
                }
                else
                {
                    throw;
                }
            }
            _log4net.Error("Error finding course in edit of id " + id);
            return new SendResponse("", StatusCodes.Status204NoContent, null, "course not found");
        }
        public async Task<SendResponse> PostCourse(Course course)
        {
            if (course == null)
            {
                _log4net.Error("Error posting course");
                return new SendResponse("", StatusCodes.Status406NotAcceptable, null, "Course cannot be posted");
            }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            _log4net.Info("Course create revoked " + course.CourseId);
            return new SendResponse("Posted course", StatusCodes.Status201Created, null, "");

        }

        public async Task<SendResponse> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                _log4net.Error("Error posting course" + id);
                return new SendResponse("", StatusCodes.Status404NotFound, null, "Cannot find any course");
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            _log4net.Error("Delete course revoked" + id);

            return new SendResponse("Deleted course successfully", StatusCodes.Status200OK, course, "");

        }
        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
