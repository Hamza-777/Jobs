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
    public class JobUnitTest
    {

        //Arrange
        List<Job> Jobs = new List<Job>();
        IQueryable<Job> jobdata;
        Mock<DbSet<Job>> mockSet;
        Mock<userDbContext> jobcontextmock;
        Mock<IJobsRepo> jobprovider;

        [SetUp]
        public void Setup()
        {

            jobdata = Jobs.AsQueryable();
            mockSet = new Mock<DbSet<Job>>();
            mockSet.As<IQueryable<Job>>().Setup(m => m.Provider).Returns(jobdata.Provider);
            mockSet.As<IQueryable<Job>>().Setup(m => m.Expression).Returns(jobdata.Expression);
            mockSet.As<IQueryable<Job>>().Setup(m => m.ElementType).Returns(jobdata.ElementType);
            mockSet.As<IQueryable<Job>>().Setup(m => m.GetEnumerator()).Returns(jobdata.GetEnumerator());
            var p = new DbContextOptions<userDbContext>();
            jobcontextmock = new Mock<userDbContext>(p);
            //coursecontextmock.Setup(x => x.Courses).Returns(mockSet.Object);
            jobprovider = new Mock<IJobsRepo>();

        }

        [Test]
        public void PutJob_TypeMatching()
        {
            Job expected = new Job();
            expected.Id = 1;
            expected.description = "Development Job in Company";
            expected.redirect_url = "url.com";
            expected.salary_max = 100000;
            expected.location = "Chennai";
            expected.title = "Developer";
            expected.salary_min = 50000;
            expected.company = "Company";
            expected.created = DateTime.Now.ToString();
            expected.stateid = 1;
            expected.cityid = 1;
            expected.categoryid = 1; 
      
            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Job actual = new Job();
            actual.Id = 1;
            actual.description = "Development Job in Company";
            actual.redirect_url = "url.com";
            actual.salary_max = 100000;
            actual.location = "Chennai";
            actual.title = "Developer";
            actual.salary_min = 50000;
            actual.company = "Company";
            actual.created = DateTime.Now.ToString();
            actual.stateid = 1;
            actual.cityid = 1;
            actual.categoryid = 1;

            string expectedResult = "{\"Value\":{\"message\":\"Edited Job Successfully\",\"code\":201,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            jobprovider.Setup(x => x.PutJob(actual.Id,actual)).Returns(Task.FromResult(new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, null, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.PutJob(actual.Id,actual);
            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            //Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void PutJob_ArgumentsNotMatching()
        {
            Job expected = new Job();
            expected.Id = 1;
            expected.description = "Development Job in Company";
            expected.redirect_url = "url.com";
            expected.salary_max = 100000;
            expected.location = "Chennai";
            expected.title = "Developer";
            expected.salary_min = 50000;
            expected.company = "Company";
            expected.created = DateTime.Now.ToString();
            expected.stateid = 1;
            expected.cityid = 1;
            expected.categoryid = 1;

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Job actual = new Job();
            actual.Id = 2;
            actual.description = "Development Job in Company";
            actual.redirect_url = "url.com";
            actual.salary_max = 100000;
            actual.location = "Chennai";
            actual.title = "Developer";
            actual.salary_min = 50000;
            actual.company = "Company";
            actual.created = DateTime.Now.ToString();
            actual.stateid = 1;
            actual.cityid = 1;
            actual.categoryid = 1;

            string expectedResult = "{\"Value\":{\"message\":\"Edited Job Successfully\",\"code\":201,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";


            jobprovider.Setup(x => x.PutJob(actual.Id,actual)).Returns(Task.FromResult(new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, actual, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.PutJob(actual.Id,actual);


            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            Assert.AreNotEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void PutJob_ArgumentsMatching()
        {
            Job expected = new Job();
            expected.Id = 1;
            expected.description = "Development Job in Company";
            expected.redirect_url = "url.com";
            expected.salary_max = 100000;
            expected.location = "Chennai";
            expected.title = "Developer";
            expected.salary_min = 50000;
            expected.company = "Company";
            expected.created = DateTime.Now.ToString();
            expected.stateid = 1;
            expected.cityid = 1;
            expected.categoryid = 1;

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Job actual = new Job();
            actual.Id = 1;
            actual.description = "Development Job in Company";
            actual.redirect_url = "url.com";
            actual.salary_max = 100000;
            actual.location = "Chennai";
            actual.title = "Developer";
            actual.salary_min = 50000;
            actual.company = "Company";
            actual.created = DateTime.Now.ToString();
            actual.stateid = 1;
            actual.cityid = 1;
            actual.categoryid = 1;

            string expectedResult = "{\"Value\":{\"message\":\"Edited Job Successfully\",\"code\":201,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";


            jobprovider.Setup(x => x.PutJob(actual.Id, actual)).Returns(Task.FromResult(new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, actual, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.PutJob(actual.Id, actual);




            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }
        [Test]
        public void GetJob_TypeMatching()
        {
            Job expected = new Job();
            expected.Id = 1;
            expected.description = "Development Job in Company";
            expected.redirect_url = "url.com";
            expected.salary_max = 100000;
            expected.location = "Chennai";
            expected.title = "Developer";
            expected.salary_min = 50000;
            expected.company = "Company";
            expected.created = DateTime.Now.ToString();
            expected.stateid = 1;
            expected.cityid = 1;
            expected.categoryid = 1;

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Job actual = new Job();
            actual.Id = 1;
            actual.description = "Development Job in Company";
            actual.redirect_url = "url.com";
            actual.salary_max = 100000;
            actual.location = "Chennai";
            actual.title = "Developer";
            actual.salary_min = 50000;
            actual.company = "Company";
            actual.created = DateTime.Now.ToString();
            actual.stateid = 1;
            actual.cityid = 1;
            actual.categoryid = 1;

            string expectedResult = "{\"Value\":{\"message\":\"jobs Found\",\"code\":201,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            jobprovider.Setup(x => x.GetJobs()).Returns(Task.FromResult(new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, null, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.GetJobs();
            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            //Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void GetJob_ArgumentsNotMatching()
        {
            Job expected = new Job();
            expected.Id = 1;
            expected.description = "Development Job in Company";
            expected.redirect_url = "url.com";
            expected.salary_max = 100000;
            expected.location = "Chennai";
            expected.title = "Developer";
            expected.salary_min = 50000;
            expected.company = "Company";
            expected.created = DateTime.Now.ToString();
            expected.stateid = 1;
            expected.cityid = 1;
            expected.categoryid = 1;

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Job actual = new Job();
            actual.Id = 2;
            actual.description = "Development Job in Company";
            actual.redirect_url = "url.com";
            actual.salary_max = 100000;
            actual.location = "Chennai";
            actual.title = "Developer";
            actual.salary_min = 50000;
            actual.company = "Company";
            actual.created = DateTime.Now.ToString();
            actual.stateid = 1;
            actual.cityid = 1;
            actual.categoryid = 1;

            string expectedResult = "{\"Value\":{\"message\":\"jobs Found\",\"code\":200,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";


            jobprovider.Setup(x => x.GetJobs()).Returns(Task.FromResult(new SendResponse("jobs Found", StatusCodes.Status200OK, actual, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.GetJobs();


            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            Assert.AreNotEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void GetJob_ArgumentsMatching()
        {
            Job expected = new Job();
            expected.Id = 1;
            expected.description = "Development Job in Company";
            expected.redirect_url = "url.com";
            expected.salary_max = 100000;
            expected.location = "Chennai";
            expected.title = "Developer";
            expected.salary_min = 50000;
            expected.company = "Company";
            expected.created = DateTime.Now.ToString();
            expected.stateid = 1;
            expected.cityid = 1;
            expected.categoryid = 1;

            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            Job actual = new Job();
            actual.Id = 1;
            actual.description = "Development Job in Company";
            actual.redirect_url = "url.com";
            actual.salary_max = 100000;
            actual.location = "Chennai";
            actual.title = "Developer";
            actual.salary_min = 50000;
            actual.company = "Company";
            actual.created = DateTime.Now.ToString();
            actual.stateid = 1;
            actual.cityid = 1;
            actual.categoryid = 1;

            string expectedResult = "{\"Value\":{\"message\":\"jobs Found\",\"code\":200,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";


            jobprovider.Setup(x => x.GetJobs()).Returns(Task.FromResult(new SendResponse("jobs Found", StatusCodes.Status200OK, actual, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.GetJobs();




            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }

        //    [Test]
        //    public void GetCourse_TypeMatching()
        //    {
        //        Course expected = new Course();
        //        expected.CourseId = 12;
        //        expected.CourseName = "Python";
        //        expected.CourseCategory = "Software";
        //        expected.CourseDescription = "Easy learning Coding Language";
        //        expected.CourseAuthor = "Hamza Rarani";
        //        expected.CourseAmount = 4999;
        //        expected.CourseImage = "imageURL";
        //        expected.CourseVideoURL = "VdoURL";

        //        //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

        //        Course actual = new Course();
        //        actual.CourseId = 12;
        //        actual.CourseName = "Python";
        //        actual.CourseCategory = "Software";
        //        actual.CourseDescription = "Easy learning Coding Language";
        //        actual.CourseAuthor = "Hamza Rarani";
        //        actual.CourseAmount = 4999;
        //        actual.CourseImage = "imageURL";
        //        actual.CourseVideoURL = "VdoURL";

        //        string expectedResult = "{\"Value\":{\"message\":\"course Found\",\"code\":200,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

        //        courseprovider.Setup(x => x.GetCourse(actual.CourseId)).Returns(Task.FromResult(new SendResponse("course Found", StatusCodes.Status200OK, actual, "")));
        //        CoursesController obj = new CoursesController(courseprovider.Object);
        //        var res = obj.GetCourse(actual.CourseId);


        //        Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
        //        //Console.WriteLine(expectedResult);
        //        //Console.WriteLine(res.Result.ToJson().ToString());
        //        //Assert.AreEqual(expected, res.Result.ToJson().ToString());
        //        //Assert.AreEqual(obj.data,p);
        //        //Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        //    }

        //    [Test]
        //    public void GetCourse_ArgumentsNotMatching()
        //    {
        //        Course expected = new Course();
        //        expected.CourseId = 11;
        //        expected.CourseName = "Python";
        //        expected.CourseCategory = "Software";
        //        expected.CourseDescription = "Easy learning Coding Language";
        //        expected.CourseAuthor = "Hamza Rarani";
        //        expected.CourseAmount = 4999;
        //        expected.CourseImage = "imageURL";
        //        expected.CourseVideoURL = "VdoURL";

        //        //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

        //        Course actual = new Course();
        //        actual.CourseId = 12;
        //        actual.CourseName = "Python";
        //        actual.CourseCategory = "Software";
        //        actual.CourseDescription = "Easy learning Coding Language";
        //        actual.CourseAuthor = "Hamza Rarani";
        //        actual.CourseAmount = 4999;
        //        actual.CourseImage = "imageURL";
        //        actual.CourseVideoURL = "VdoURL";

        //        string expectedResult = "{\"Value\":{\"message\":\"course Found\",\"code\":200,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

        //        courseprovider.Setup(x => x.GetCourse(actual.CourseId)).Returns(Task.FromResult(new SendResponse("course Found", StatusCodes.Status200OK, actual, "")));
        //        CoursesController obj = new CoursesController(courseprovider.Object);
        //        var res = obj.GetCourse(actual.CourseId);
        //        //Console.WriteLine(expectedResult);
        //        //Console.WriteLine("Result:"+res.Result.ToJson().ToString());

        //        //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
        //        //Console.WriteLine(res.Result.ToJson().ToString());
        //        //Assert.AreEqual(expected, res.Result.ToJson().ToString());
        //        //Assert.AreEqual(obj.data,p);
        //        //Console.WriteLine(expectedResult);
        //        //Console.WriteLine(res.Result.ToJson().ToString());
        //        Assert.AreNotEqual(expectedResult, res.Result.ToJson().ToString());


        //    }

        //    [Test]
        //    public void GetCourse_ArgumentsMatching()
        //    {
        //        Course expected = new Course();
        //        expected.CourseId = 12;
        //        expected.CourseName = "Python";
        //        expected.CourseCategory = "Software";
        //        expected.CourseDescription = "Easy learning Coding Language";
        //        expected.CourseAuthor = "Hamza Rarani";
        //        expected.CourseAmount = 4999;
        //        expected.CourseImage = "imageURL";
        //        expected.CourseVideoURL = "VdoURL";

        //        //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

        //        Course actual = new Course();
        //        actual.CourseId = 12;
        //        actual.CourseName = "Python";
        //        actual.CourseCategory = "Software";
        //        actual.CourseDescription = "Easy learning Coding Language";
        //        actual.CourseAuthor = "Hamza Rarani";
        //        actual.CourseAmount = 4999;
        //        actual.CourseImage = "imageURL";
        //        actual.CourseVideoURL = "VdoURL";

        //        string expectedResult = "{\"Value\":{\"message\":\"course Found\",\"code\":200,\"data\":" + expected.ToJson().ToString() + ",\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

        //        courseprovider.Setup(x => x.GetCourse(actual.CourseId)).Returns(Task.FromResult(new SendResponse("course Found", StatusCodes.Status200OK, actual, "")));
        //        CoursesController obj = new CoursesController(courseprovider.Object);
        //        var res = obj.GetCourse(actual.CourseId);


        //        //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
        //        //Console.WriteLine(res.Result.ToJson().ToString());
        //        //Assert.AreEqual(expected, res.Result.ToJson().ToString());
        //        //Assert.AreEqual(obj.data,p);
        //        //Console.WriteLine(expectedResult);
        //        //Console.WriteLine(res.Result.ToJson().ToString());
        //        Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        //    }

    }
}

