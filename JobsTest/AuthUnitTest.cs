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
using JobsAPI.Hashing;
namespace Test_JobsAPI
{
    public class AuthUnitTest
    {

        //Arrange
        List<user> user = new List<user>();
        IQueryable<user> userdata;
        Mock<DbSet<userDbContext>> mockSet;
        Mock<userDbContext> usercontextmock;
        Mock<IAuthRepo> userprovider;
        HashMethods hm;

        [SetUp]
        public void Setup()
        {

            userdata = user.AsQueryable();
            mockSet = new Mock<DbSet<userDbContext>>();
            mockSet.As<IQueryable<user>>().Setup(m => m.Provider).Returns(userdata.Provider);
            mockSet.As<IQueryable<user>>().Setup(m => m.Expression).Returns(userdata.Expression);
            mockSet.As<IQueryable<user>>().Setup(m => m.ElementType).Returns(userdata.ElementType);
            mockSet.As<IQueryable<user>>().Setup(m => m.GetEnumerator()).Returns(userdata.GetEnumerator());
            var p = new DbContextOptions<userDbContext>();
            usercontextmock = new Mock<userDbContext>(p);
            //coursecontextmock.Setup(x => x.Courses).Returns(mockSet.Object);
            userprovider = new Mock<IAuthRepo>();
            hm = new HashMethods();

        }

        [Test]
        public void GetUserByUsername_TypeMatching()
        {
            user expected = new user();
            expected.UserID = 1;
            expected.UserName = "name";
            expected.Password = "password";
            expected.EmailId = "name@email.com";
            expected.MobileNumber = 1234567890;
            expected.Role = "Recruiter";
            expected.CompanyName = "Company";
            expected.RecruiterDescription = "Description";
            expected.Salt = hm.GenerateSalt();
            expected.Password  =  Convert.ToBase64String(hm.GetHash(expected.Password, expected.Salt));

            user actual = new user();
            actual.UserID = 1;
            actual.UserName = "name";
            actual.Password = "password";
            actual.EmailId = "name@email.com";
            actual.MobileNumber = 1234567890;
            actual.Role = "Recruiter";
            actual.CompanyName = "Company";
            actual.RecruiterDescription = "Description";
            actual.Salt = expected.Salt;
            actual.Password = Convert.ToBase64String(hm.GetHash(actual.Password, actual.Salt));
            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            SendResponse sendResponse = new SendResponse("Found username", StatusCodes.Status200OK, expected, "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            userprovider.Setup(x => x.GetbyUsername(expected.UserName)).Returns(Task.FromResult(new SendResponse("Found username", StatusCodes.Status200OK, actual, "")));
            AuthController obj = new AuthController(userprovider.Object);
            var res = obj.GetbyUsername(expected.UserName);
            Assert.That(res, Is.InstanceOf<Task>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            //Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());
        }

        [Test]
        public void GetUserByUsername_ArgumentsNotMatching()
        {

            user expected = new user();
            expected.UserID = 1;
            expected.UserName = "name";
            expected.Password = "password";
            expected.EmailId = "name@email.com";
            expected.MobileNumber = 1234567890;
            expected.Role = "Recruiter";
            expected.CompanyName = "Company";
            expected.RecruiterDescription = "Description";
            expected.Salt = hm.GenerateSalt();
            expected.Password = Convert.ToBase64String(hm.GetHash(expected.Password, expected.Salt));

            user actual = new user();
            actual.UserID = 2;
            actual.UserName = "name";
            actual.Password = "password";
            actual.EmailId = "name@email.com";
            actual.MobileNumber = 1234567890;
            actual.Role = "Recruiter";
            actual.CompanyName = "Company";
            actual.RecruiterDescription = "Description";
            actual.Salt = expected.Salt;
            actual.Password = Convert.ToBase64String(hm.GetHash(actual.Password, actual.Salt));
            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            SendResponse sendResponse = new SendResponse("Found username", StatusCodes.Status200OK, expected, "");
            string expectedResult = "{\"Result\":{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}}";
            userprovider.Setup(x => x.GetbyUsername(expected.UserName)).Returns(Task.FromResult(new SendResponse("Found username", StatusCodes.Status200OK, actual, "")));
            AuthController obj = new AuthController(userprovider.Object);
            var res = obj.GetbyUsername(expected.UserName);           
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            Assert.AreNotEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void GetUserByUsername_ArgumentsMatching()
        {
            user expected = new user();
            expected.UserID = 1;
            expected.UserName = "name";
            expected.Password = "password";
            expected.EmailId = "name@email.com";
            expected.MobileNumber = 1234567890;
            expected.Role = "Recruiter";
            expected.CompanyName = "Company";
            expected.RecruiterDescription = "Description";
            expected.Salt = hm.GenerateSalt();
            expected.Password = Convert.ToBase64String(hm.GetHash(expected.Password, expected.Salt));

            user actual = new user();
            actual.UserID = 1;
            actual.UserName = "name";
            actual.Password = "password";
            actual.EmailId = "name@email.com";
            actual.MobileNumber = 1234567890;
            actual.Role = "Recruiter";
            actual.CompanyName = "Company";
            actual.RecruiterDescription = "Description";
            actual.Salt = expected.Salt;
            actual.Password = Convert.ToBase64String(hm.GetHash(actual.Password, actual.Salt));
            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            SendResponse sendResponse = new SendResponse("Found username", StatusCodes.Status200OK, expected, "");
            string expectedResult = "{\"Result\":{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}}";
            
            userprovider.Setup(x => x.GetbyUsername(expected.UserName)).Returns(Task.FromResult(new SendResponse("Found username", StatusCodes.Status200OK, actual, "")));
            AuthController obj = new AuthController(userprovider.Object);
            var res = obj.GetbyUsername(expected.UserName);
            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }
    }


}


