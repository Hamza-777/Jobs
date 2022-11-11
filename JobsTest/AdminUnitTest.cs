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
    public class AdminUnitTest
    {

        //Arrange
        List<user> user = new List<user>();
        IQueryable<user> userdata;
        Mock<DbSet<userDbContext>> mockSet;
        Mock<userDbContext> usercontextmock;
        Mock<IAdminRepo> userprovider;
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
            userprovider = new Mock<IAdminRepo>();
            hm = new HashMethods();

        }

        [Test]
        public void RegisterAdmin_TypeMatching()
        {
            user expected = new user();
            expected.UserID = 1;
            expected.UserName = "name";
            expected.Password = "password";
            expected.EmailId = "name@email.com";
            expected.MobileNumber = 1234567890;
            expected.Role = "Admin";
            expected.Salt = hm.GenerateSalt();
            expected.Password  =  Convert.ToBase64String(hm.GetHash(expected.Password, expected.Salt));

            user actual = new user();
            actual.UserID = 1;
            actual.UserName = "name";
            actual.Password = "password";
            actual.EmailId = "name@email.com";
            actual.MobileNumber = 1234567890;
            actual.Role = "Admin";
            actual.Salt = expected.Salt;
            actual.Password = Convert.ToBase64String(hm.GetHash(actual.Password, actual.Salt));
            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            SendResponse sendResponse = new SendResponse("Registered Successfully ", StatusCodes.Status201Created, expected, "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            userprovider.Setup(x => x.RegisterAdmin(expected)).Returns(Task.FromResult(new SendResponse("Registered Successfully ", StatusCodes.Status201Created, actual, "")));
            AdminController obj = new AdminController(userprovider.Object);
            var res = obj.RegisterAdmin(expected);
            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            //Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());
        }

        [Test]
        public void RegisterAdmin_ArgumentsNotMatching()
        {

            user expected = new user();
            expected.UserID = 1;
            expected.UserName = "name";
            expected.Password = "password";
            expected.EmailId = "name@email.com";
            expected.MobileNumber = 1234567890;
            expected.Role = "Admin";
            expected.Salt = hm.GenerateSalt();
            expected.Password = Convert.ToBase64String(hm.GetHash(expected.Password, expected.Salt));

            user actual = new user();
            actual.UserID =  2;
            actual.UserName = "name";
            actual.Password = "password";
            actual.EmailId = "name@email.com";
            actual.MobileNumber = 1234567890;
            actual.Role = "Admin";
            actual.Salt = expected.Salt;
            actual.Password = Convert.ToBase64String(hm.GetHash(actual.Password, actual.Salt));
            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            SendResponse sendResponse = new SendResponse("Registered Successfully ", StatusCodes.Status201Created, expected, "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            userprovider.Setup(x => x.RegisterAdmin(expected)).Returns(Task.FromResult(new SendResponse("Registered Successfully ", StatusCodes.Status201Created, actual, "")));
            AdminController obj = new AdminController(userprovider.Object);
            var res = obj.RegisterAdmin(expected);            
            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            Assert.AreNotEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void RegisterAdmin_ArgumentsMatching()
        {

            user expected = new user();
            expected.UserID = 1;
            expected.UserName = "name";
            expected.Password = "password";
            expected.EmailId = "name@email.com";
            expected.MobileNumber = 1234567890;
            expected.Role = "Admin";
            expected.Salt = hm.GenerateSalt();
            expected.Password = Convert.ToBase64String(hm.GetHash(expected.Password, expected.Salt));

            user actual = new user();
            actual.UserID = 1;
            actual.UserName = "name";
            actual.Password = "password";
            actual.EmailId = "name@email.com";
            actual.MobileNumber = 1234567890;
            actual.Role = "Admin";
            actual.Salt = expected.Salt;
            actual.Password = Convert.ToBase64String(hm.GetHash(actual.Password, actual.Salt));
            //string expected = "{\"Value\":{\"message\":\"Posted course\",\"code\":201,\"data\":{\"CourseId\":12,\"CourseName\":\"Python\",\"CourseCategory\":\"Software\",\"CourseDescription\":\"Easy learning Coding Language\",\"CourseAuthor\":\"Hamza Rarani\",\"CourseAmount\":4999.0,\"CourseImage\":\"imageURL\",\"CourseVideoURL\":\"VdoURL\"},\"error\":\"\"},\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            SendResponse sendResponse = new SendResponse("Registered Successfully ", StatusCodes.Status201Created, expected, "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            userprovider.Setup(x => x.RegisterAdmin(expected)).Returns(Task.FromResult(new SendResponse("Registered Successfully ", StatusCodes.Status201Created, actual, "")));
            AdminController obj = new AdminController(userprovider.Object);
            var res = obj.RegisterAdmin(expected);

            //Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
            //Console.WriteLine(res.Result.ToJson().ToString());
            //Assert.AreEqual(expected, res.Result.ToJson().ToString());
            //Assert.AreEqual(obj.data,p);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());


        }
    }


}


