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
    public class JobUnitTest
    {

        //Arrange
        List<Job> Jobs = new List<Job>();
        
        Mock<IJobsRepo> jobprovider;

        [SetUp]
        public void Setup()
        {

            
            jobprovider = new Mock<IJobsRepo>();

        }

        [Test]
        public void PutJob_TypeMatching()
        {
            
            
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
            
            jobprovider.Setup(x => x.PutJob(actual.Id,actual)).Returns(Task.FromResult(new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, null, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.PutJob(actual.Id,actual);
            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
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
            SendResponse sendResponse = new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, expected, "");
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
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            jobprovider.Setup(x => x.PutJob(actual.Id,actual)).Returns(Task.FromResult(new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, actual, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.PutJob(actual.Id,actual);
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
            SendResponse sendResponse = new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, expected, "");
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
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            jobprovider.Setup(x => x.PutJob(actual.Id, actual)).Returns(Task.FromResult(new SendResponse("Edited Job Successfully", StatusCodes.Status201Created, actual, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.PutJob(actual.Id, actual);
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
            SendResponse sendResponse = new SendResponse("jobs Found", StatusCodes.Status200OK, expected, "");
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
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            jobprovider.Setup(x => x.GetJobs()).Returns(Task.FromResult(new SendResponse("jobs Found", StatusCodes.Status200OK, actual, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.GetJobs();
            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());

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
            SendResponse sendResponse = new SendResponse("jobs Found", StatusCodes.Status200OK, expected, "");
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
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            jobprovider.Setup(x => x.GetJobs()).Returns(Task.FromResult(new SendResponse("jobs Found", StatusCodes.Status200OK, actual, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.GetJobs();
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
            SendResponse sendResponse = new SendResponse("jobs Found", StatusCodes.Status200OK, expected, "");
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
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            jobprovider.Setup(x => x.GetJobs()).Returns(Task.FromResult(new SendResponse("jobs Found", StatusCodes.Status200OK, actual, "")));
            JobsController obj = new JobsController(jobprovider.Object);
            var res = obj.GetJobs();
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());

        }

    }
}

