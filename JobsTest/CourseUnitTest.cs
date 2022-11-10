using JobsAPI.Controllers;
using JobsAPI.Models;
using JobsAPI.Repositories;
using JobsAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Text.Json;

namespace Test_JobsAPI
{
    public class CourseUnitTest
    {

        //Arrange
        List<Course> Courses = new List<Course>();
        IQueryable<Course> coursedata;
        Mock<DbSet<Course>> mockSet;
        Mock<userDbContext> coursecontextmock;
        Mock<ICourseRepo> courseprovider;

        [SetUp]
        public void Setup()
        {
            Courses = new List<Course>();
            coursedata = Courses.AsQueryable();
            mockSet = new Mock<DbSet<Course>>();
            mockSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(coursedata.Provider);
            mockSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(coursedata.Expression);
            mockSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(coursedata.ElementType);
            mockSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(coursedata.GetEnumerator());
            var p = new DbContextOptions<userDbContext>();
            coursecontextmock = new Mock<userDbContext>(p);
            //coursecontextmock.Setup(x => x.Courses).Returns(mockSet.Object);
            courseprovider = new Mock<ICourseRepo>();

        }

        [Test]
        public void AddCourse_TypeMatching()
        {
            Course expected = new Course();
            expected.CourseId = 12;
            expected.CourseName = "Python";
            expected.CourseCategory = "Software";
            expected.CourseDescription = "Easy learning Coding Language";
            expected.CourseAuthor = "Hamza Rarani";
            expected.CourseAmount = 4999;
            expected.CourseImage = "imageURL";
            expected.CourseVideoURL = "VdoURL";

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";

            string expectedResult = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            courseprovider.Setup(x => x.PostCourse(actual)).Returns(Task.FromResult(new SendResponse("Posted course", StatusCodes.Status201Created, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.PostCourse(actual);


            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            //Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void AddCourse_ArgumentsNotMatching()
        {
            Course expected = new Course();
            expected.CourseId = 11;
            expected.CourseName = "Python";
            expected.CourseCategory = "Software";
            expected.CourseDescription = "Easy learning Coding Language";
            expected.CourseAuthor = "Hamza Rarani";
            expected.CourseAmount = 4999;
            expected.CourseImage = "imageURL";
            expected.CourseVideoURL = "VdoURL";

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";

            string expectedResult = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            courseprovider.Setup(x => x.PostCourse(actual)).Returns(Task.FromResult(new SendResponse("Posted course", StatusCodes.Status201Created, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.PostCourse(actual);


            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            Assert.AreNotEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void AddCourse_ArgumentsMatching()
        {
            Course expected = new Course();
            expected.CourseId = 12;
            expected.CourseName = "Python";
            expected.CourseCategory = "Software";
            expected.CourseDescription = "Easy learning Coding Language";
            expected.CourseAuthor = "Hamza Rarani";
            expected.CourseAmount = 4999;
            expected.CourseImage = "imageURL";
            expected.CourseVideoURL = "VdoURL";

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";

            string expectedResult = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            courseprovider.Setup(x => x.PostCourse(actual)).Returns(Task.FromResult(new SendResponse("Posted course", StatusCodes.Status201Created, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.PostCourse(actual);


            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }

        //[Test]
        //public void AddCourse_IsNull()
        //{
        //    Course expected = new Course();
        //    //expected.CourseId = 12;
        //    //expected.CourseName = "Python";
        //    //expected.CourseCategory = "Software";
        //    //expected.CourseDescription = "Easy learning Coding Language";
        //    //expected.CourseAuthor = "Hamza Rarani";
        //    //expected.CourseAmount = 4999;
        //    //expected.CourseImage = "imageURL";
        //    //expected.CourseVideoURL = "VdoURL";

        //    //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

        //    Course actual = new Course();
        //    //actual.CourseId = 12;
        //    //actual.CourseName = "Python";
        //    //actual.CourseCategory = "Software";
        //    //actual.CourseDescription = "Easy learning Coding Language";
        //    //actual.CourseAuthor = "Hamza Rarani";
        //    //actual.CourseAmount = 4999;
        //    //actual.CourseImage = "imageURL";
        //    //actual.CourseVideoURL = "VdoURL";

        //    string expectedResult = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

        //    courseprovider.Setup(x => x.PostCourse(actual)).Returns(Task.FromResult(new SendResponse("Posted course", StatusCodes.Status201Created, actual, "")));
        //    CoursesController obj = new CoursesController(courseprovider.Object);
        //    var res = obj.PostCourse(actual);


        //    //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
        //    //Console.WriteLine(res.Result.ToJson().ToString());
        //    //Assert.AreEqual(expected, res.Result.ToJson().ToString());
        //    //Assert.AreEqual(obj.data,p);
        //    Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());
        //    //Assert.IsNull(res.);


        //}

        //[Test]
        //public void AddCourse_IsNotNull()
        //{
        //    Course expected = new Course();
        //    expected.CourseId = 12;
        //    expected.CourseName = "Python";
        //    expected.CourseCategory = "Software";
        //    expected.CourseDescription = "Easy learning Coding Language";
        //    expected.CourseAuthor = "Hamza Rarani";
        //    expected.CourseAmount = 4999;
        //    expected.CourseImage = "imageURL";
        //    expected.CourseVideoURL = "VdoURL";

        //    //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

        //    Course actual = new Course();
        //    //actual.CourseId = 12;
        //    //actual.CourseName = "Python";
        //    //actual.CourseCategory = "Software";
        //    //actual.CourseDescription = "Easy learning Coding Language";
        //    //actual.CourseAuthor = "Hamza Rarani";
        //    //actual.CourseAmount = 4999;
        //    //actual.CourseImage = "imageURL";
        //    //actual.CourseVideoURL = "VdoURL";

        //    string expectedResult = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

        //    courseprovider.Setup(x => x.PostCourse(actual)).Returns(Task.FromResult(new SendResponse("Posted course", StatusCodes.Status201Created, actual, "")));
        //    CoursesController obj = new CoursesController(courseprovider.Object);
        //    var res = obj.PostCourse(actual);


        //    //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
        //    //Console.WriteLine(res.Result.ToJson().ToString());
        //    //Assert.AreEqual(expected, res.Result.ToJson().ToString());
        //    //Assert.AreEqual(obj.data,p);
        //    Assert.AreNotEqual(expectedResult, res.Result.ToJson().ToString());
        //    //Assert.IsNull(res.);


        //}
        [Test]
        public void GetCourse_TypeMatching()
        {
            Course expected = new Course();
            expected.CourseId = 12;
            expected.CourseName = "Python";
            expected.CourseCategory = "Software";
            expected.CourseDescription = "Easy learning Coding Language";
            expected.CourseAuthor = "Hamza Rarani";
            expected.CourseAmount = 4999;
            expected.CourseImage = "imageURL";
            expected.CourseVideoURL = "VdoURL";

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";

            string expectedResult = "{\"Value\":{\"message\":\"course Found\",\"code\":200,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            courseprovider.Setup(x => x.GetCourse(actual.CourseId)).Returns(Task.FromResult(new SendResponse("course Found", StatusCodes.Status200OK, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.GetCourse(actual.CourseId);


            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(expectedResult);
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            //Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void GetCourse_ArgumentsNotMatching()
        {
            Course expected = new Course();
            expected.CourseId = 11;
            expected.CourseName = "Python";
            expected.CourseCategory = "Software";
            expected.CourseDescription = "Easy learning Coding Language";
            expected.CourseAuthor = "Hamza Rarani";
            expected.CourseAmount = 4999;
            expected.CourseImage = "imageURL";
            expected.CourseVideoURL = "VdoURL";

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";

            string expectedResult = "{\"Value\":{\"message\":\"course Found\",\"code\":200,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            courseprovider.Setup(x => x.GetCourse(actual.CourseId)).Returns(Task.FromResult(new SendResponse("course Found", StatusCodes.Status200OK, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.GetCourse(actual.CourseId);
            //Console.WriteLine(expectedResult);
            //Console.WriteLine("Result:"+res.Result.ToJson().ToString());

            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            //Console.WriteLine(expectedResult);
            //Console.WriteLine(res.Result.ToJson().ToString());
            Assert.AreNotEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void GetCourse_ArgumentsMatching()
        {
            Course expected = new Course();
            expected.CourseId = 12;
            expected.CourseName = "Python";
            expected.CourseCategory = "Software";
            expected.CourseDescription = "Easy learning Coding Language";
            expected.CourseAuthor = "Hamza Rarani";
            expected.CourseAmount = 4999;
            expected.CourseImage = "imageURL";
            expected.CourseVideoURL = "VdoURL";

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";
            
            string expectedResult = "{\"Value\":{\"message\":\"course Found\",\"code\":200,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            courseprovider.Setup(x => x.GetCourse(actual.CourseId)).Returns(Task.FromResult(new SendResponse("course Found", StatusCodes.Status200OK, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.GetCourse(actual.CourseId);


            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            //Console.WriteLine(expectedResult);
            //Console.WriteLine(res.Result.ToJson().ToString());
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }

    }
}

