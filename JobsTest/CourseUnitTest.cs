using JobsAPI.Controllers;
using JobsAPI.Models;
using JobsAPI.Repositories;
using JobsAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NuGet.Protocol;

namespace Test_JobsAPI
{
    public class CourseUnitTest
    {

        //Arrange
        List<Course> Courses = new List<Course>();
        Mock<ICourseRepo> courseprovider;

        [SetUp]
        public void Setup()
        {
            Courses = new List<Course>();
            courseprovider = new Mock<ICourseRepo>();

        }

        [Test]
        public void AddCourse_TypeMatching()
        {
            
            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";
            courseprovider.Setup(x => x.PostCourse(actual)).Returns(Task.FromResult(new SendResponse("Posted course", StatusCodes.Status201Created, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.PostCourse(actual);    
            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
        }

        [Test]
        public void AddCourse_ArgumentsNotMatching()
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
            SendResponse sendResponse = new SendResponse("Posted course", StatusCodes.Status201Created, expected, "");
            Course actual = new Course();
            actual.CourseId = 13;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            courseprovider.Setup(x => x.PostCourse(actual)).Returns(Task.FromResult(new SendResponse("Posted course", StatusCodes.Status201Created, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.PostCourse(actual);
            Console.WriteLine(res.Result.ToJson().ToString());
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
            SendResponse sendResponse = new SendResponse("Posted course", StatusCodes.Status201Created, expected, "");
            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            courseprovider.Setup(x => x.PostCourse(actual)).Returns(Task.FromResult(new SendResponse("Posted course", StatusCodes.Status201Created, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.PostCourse(actual);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }


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
            SendResponse sendResponse = new SendResponse("course Found", StatusCodes.Status200OK, expected, "");
            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            courseprovider.Setup(x => x.GetCourse(actual.CourseId)).Returns(Task.FromResult(new SendResponse("course Found", StatusCodes.Status200OK, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.GetCourse(actual.CourseId);
            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
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
            SendResponse sendResponse = new SendResponse("course Found", StatusCodes.Status200OK, expected, "");
            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            courseprovider.Setup(x => x.GetCourse(actual.CourseId)).Returns(Task.FromResult(new SendResponse("course Found", StatusCodes.Status200OK, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.GetCourse(actual.CourseId);
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
            SendResponse sendResponse = new SendResponse("course Found", StatusCodes.Status200OK, expected, "");
            Course actual = new Course();
            actual.CourseId = 12;
            actual.CourseName = "Python";
            actual.CourseCategory = "Software";
            actual.CourseDescription = "Easy learning Coding Language";
            actual.CourseAuthor = "Hamza Rarani";
            actual.CourseAmount = 4999;
            actual.CourseImage = "imageURL";
            actual.CourseVideoURL = "VdoURL";
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            courseprovider.Setup(x => x.GetCourse(actual.CourseId)).Returns(Task.FromResult(new SendResponse("course Found", StatusCodes.Status200OK, actual, "")));
            CoursesController obj = new CoursesController(courseprovider.Object);
            var res = obj.GetCourse(actual.CourseId);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());

        }

    }
}

