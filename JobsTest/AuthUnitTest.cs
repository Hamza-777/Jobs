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
            SendResponse sendResponse = new SendResponse("Found username", StatusCodes.Status200OK, expected, "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            userprovider.Setup(x => x.GetbyUsername(expected.UserName)).Returns(Task.FromResult(new SendResponse("Found username", StatusCodes.Status200OK, actual, "")));
            AuthController obj = new AuthController(userprovider.Object);
            var res = obj.GetbyUsername(expected.UserName);
            Assert.That(res, Is.InstanceOf<Task>());
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
            SendResponse sendResponse = new SendResponse("Found username", StatusCodes.Status200OK, expected, "");
            string expectedResult = "{\"Result\":{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}}";
            userprovider.Setup(x => x.GetbyUsername(expected.UserName)).Returns(Task.FromResult(new SendResponse("Found username", StatusCodes.Status200OK, actual, "")));
            AuthController obj = new AuthController(userprovider.Object);
            var res = obj.GetbyUsername(expected.UserName);           
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
            SendResponse sendResponse = new SendResponse("Found username", StatusCodes.Status200OK, expected, "");
            string expectedResult = "{\"Result\":{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}}";
            userprovider.Setup(x => x.GetbyUsername(expected.UserName)).Returns(Task.FromResult(new SendResponse("Found username", StatusCodes.Status200OK, actual, "")));
            AuthController obj = new AuthController(userprovider.Object);
            var res = obj.GetbyUsername(expected.UserName);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());
        }
    }
}


