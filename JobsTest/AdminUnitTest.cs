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
            SendResponse sendResponse = new SendResponse("Registered Successfully ", StatusCodes.Status200OK, user, "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            userprovider.Setup(x => x.RegisterAdmin(actual)).Returns(Task.FromResult(new SendResponse("Registered Successfully ", StatusCodes.Status200OK, user, "")));
            AdminController obj = new AdminController(userprovider.Object);
            var res = obj.RegisterAdmin(expected);
            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
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
            SendResponse sendResponse = new SendResponse("Registered Successfully ", StatusCodes.Status201Created, expected, "");
            Console.WriteLine(sendResponse.ToJson().ToString());
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            userprovider.Setup(x => x.RegisterAdmin(expected)).Returns(Task.FromResult(new SendResponse("Registered Successfully ", StatusCodes.Status201Created, expected, "")));
            AdminController obj = new AdminController(userprovider.Object);
            var res = obj.RegisterAdmin(actual);
            Console.WriteLine(res.Result.ToJson().ToString());
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
            SendResponse sendResponse = new SendResponse("Registered Successfully ", StatusCodes.Status201Created, expected, "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            userprovider.Setup(x => x.RegisterAdmin(actual)).Returns(Task.FromResult(new SendResponse("Registered Successfully ", StatusCodes.Status201Created, actual, "")));
            AdminController obj = new AdminController(userprovider.Object);
            var res = obj.RegisterAdmin(expected);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());
        }
    }


}


