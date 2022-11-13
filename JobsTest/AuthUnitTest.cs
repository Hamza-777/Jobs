using JobsAPI.Controllers;
using JobsAPI.Models;
using JobsAPI.Repositories;
using JobsAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Moq;
using NuGet.Protocol;
using JobsAPI.Hashing;
namespace Test_JobsAPI
{
    public class AuthUnitTest
    { 
        //Arrange
        List<user> user = new List<user>();
        Mock<IAuthRepo> userprovider;
        HashMethods hm;

        [SetUp]
        public void Setup()
        {

            
            userprovider = new Mock<IAuthRepo>();
            hm = new HashMethods();

        }

        [Test]
        public void GetUserByUsername_TypeMatching()
        {
            
            user actual = new user();
            actual.UserID = 1;
            actual.UserName = "name";
            actual.Password = "password";
            actual.EmailId = "name@email.com";
            actual.MobileNumber = 1234567890;
            actual.Role = "Recruiter";
            actual.CompanyName = "Company";
            actual.RecruiterDescription = "Description";
            actual.Salt = hm.GenerateSalt();
            actual.Password = Convert.ToBase64String(hm.GetHash(actual.Password, actual.Salt));
            
            userprovider.Setup(x => x.GetbyUsername(actual.UserName)).Returns(Task.FromResult(new SendResponse("Found username", StatusCodes.Status200OK, actual, "")));
            AuthController obj = new AuthController(userprovider.Object);
            var res = obj.GetbyUsername(actual.UserName);
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
            userprovider.Setup(x => x.GetbyUsername(actual.UserName)).Returns(Task.FromResult(new SendResponse("Found username", StatusCodes.Status200OK, actual, "")));
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
            userprovider.Setup(x => x.GetbyUsername(actual.UserName)).Returns(Task.FromResult(new SendResponse("Found username", StatusCodes.Status200OK, actual, "")));
            AuthController obj = new AuthController(userprovider.Object);
            var res = obj.GetbyUsername(expected.UserName);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());
        }
    }
}


