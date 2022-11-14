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
    public class OtpUnitTest
    {

        //Arrange
        List<Otp> Otp = new List<Otp>();
        Mock<IOtpRepo> otpprovider;

        [SetUp]
        public void Setup()
        {

            
            otpprovider = new Mock<IOtpRepo>();

        }

        [Test]
        public void ClearOtp_TypeMatching()
        {

            
            Otp otp2 = new Otp();
            otp2.Otpid = 2;
            otp2.value = "987654";
            otp2.Timestamp = DateTime.Now.AddMinutes(5);
            
            List<Otp> actual = new List<Otp>
            {
                otp2
            };



            

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

