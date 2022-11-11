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
    public class OtpUnitTest
    {

        //Arrange
        List<Otp> Otp = new List<Otp>();
        IQueryable<Otp> otpdata;
        Mock<DbSet<Otp>> mockSet;
        Mock<userDbContext> otpcontextmock;
        Mock<IOtpRepo> otpprovider;

        [SetUp]
        public void Setup()
        {

            otpdata = Otp.AsQueryable();
            mockSet = new Mock<DbSet<Otp>>();
            mockSet.As<IQueryable<Otp>>().Setup(m => m.Provider).Returns(otpdata.Provider);
            mockSet.As<IQueryable<Otp>>().Setup(m => m.Expression).Returns(otpdata.Expression);
            mockSet.As<IQueryable<Otp>>().Setup(m => m.ElementType).Returns(otpdata.ElementType);
            mockSet.As<IQueryable<Otp>>().Setup(m => m.GetEnumerator()).Returns(otpdata.GetEnumerator());
            var p = new DbContextOptions<userDbContext>();
            otpcontextmock = new Mock<userDbContext>(p);
            otpprovider = new Mock<IOtpRepo>();

        }

        [Test]
        public void ClearOtp_TypeMatching()
        {

            Otp otp1 = new Otp();
            otp1.Otpid = 1;
            otp1.value = "123456";
            otp1.Timestamp = DateTime.Now;
            Otp otp2 = new Otp();
            otp2.Otpid = 2;
            otp2.value = "987654";
            otp2.Timestamp = DateTime.Now.AddMinutes(5);
            List<Otp> expected = new List<Otp>
            {
                otp2
            };
            List<Otp> actual = new List<Otp>
            {
                otp2
            };



            SendResponse sendResponse = new SendResponse("Otp Cleared successfully", StatusCodes.Status205ResetContent, expected.ToJson(), "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            otpprovider.Setup(x => x.ClearOTP()).Returns(Task.FromResult(new SendResponse("Otp Cleared successfully", StatusCodes.Status205ResetContent, actual.ToJson(), "")));
            OtpController obj = new OtpController(otpprovider.Object);
            var res = obj.ClearOTP();
            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
        }

        [Test]
        public void ClearOtp_ArgumentsNotMatching()
        {

            Otp otp1 = new Otp();
            otp1.Otpid = 1;
            otp1.value = "123456";
            otp1.Timestamp = DateTime.Now;
            Otp otp2 = new Otp();
            otp2.Otpid = 2;
            otp2.value = "987654";
            otp2.Timestamp = DateTime.Now.AddMinutes(5);
            List<Otp> expected = new List<Otp>
            {
                otp2
            };
            List<Otp> actual = new List<Otp>
            {
                otp1
            };
            SendResponse sendResponse = new SendResponse("Otp Cleared successfully", StatusCodes.Status205ResetContent, expected.ToJson(), "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";

            otpprovider.Setup(x => x.ClearOTP()).Returns(Task.FromResult(new SendResponse("Otp Cleared successfully", StatusCodes.Status205ResetContent, actual.ToJson(), "")));
            OtpController obj = new OtpController(otpprovider.Object);
            var res = obj.ClearOTP();
            Assert.AreNotEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void Clear_OtpArgumentsMatching()
        {

            Otp otp1 = new Otp();
            otp1.Otpid = 1;
            otp1.value = "123456";
            otp1.Timestamp = DateTime.Now;
            Otp otp2 = new Otp();
            otp2.Otpid = 2;
            otp2.value = "987654";
            otp2.Timestamp = DateTime.Now.AddMinutes(5);
            List<Otp> expected = new List<Otp>
            {
                otp2
            };
            List<Otp> actual = new List<Otp>
            {
                otp2
            };
            SendResponse sendResponse = new SendResponse("Otp Cleared successfully", StatusCodes.Status205ResetContent, expected.ToJson(), "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            otpprovider.Setup(x => x.ClearOTP()).Returns(Task.FromResult(new SendResponse("Otp Cleared successfully", StatusCodes.Status205ResetContent, actual.ToJson(), "")));
            OtpController obj = new OtpController(otpprovider.Object);
            var res = obj.ClearOTP();
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());
        }


    }
}

