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
    public class BlogUnitTest
    {

        //Arrange
        List<Blog> Blogs = new List<Blog>();
        
        Mock<IBlogsRepo> blogprovider;

        [SetUp]
        public void Setup()
        {
            
            blogprovider = new Mock<IBlogsRepo>();

        }

        [Test]
        public void DeleteBlog_TypeMatching()
        {
            
            Blog actual = new Blog();
            actual.BlogId = 1;
            actual.BlogTitle = "Blog Title";
            actual.BlogContent = "Blog Content";
            actual.CoverImage = "image.com";
            actual.UserID = 1;
            
            
            blogprovider.Setup(x => x.DeleteBlog(actual.BlogId)).Returns(Task.FromResult(new SendResponse("Deleted blog successfully", StatusCodes.Status200OK, actual, "")));
            BlogsController obj = new BlogsController(blogprovider.Object);
            var res = obj.DeleteBlog(actual.BlogId);
            Assert.That(res, Is.InstanceOf<Task<IActionResult>>());
        }

        [Test]
        public void DeleteBlog_ArgumentsNotMatching()
        {
            Blog expected = new Blog();
            expected.BlogId = 1;
            expected.BlogTitle = "Blog Title";
            expected.BlogContent = "Blog Content";
            expected.CoverImage = "image.com";
            expected.UserID = 1;
            Blog actual = new Blog();
            actual.BlogId = 2;
            actual.BlogTitle = "Blog Title";
            actual.BlogContent = "Blog Content";
            actual.CoverImage = "image.com";
            actual.UserID = 1;
            SendResponse sendResponse = new SendResponse("Deleted blog successfully", StatusCodes.Status200OK, expected, "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            blogprovider.Setup(x => x.DeleteBlog(actual.BlogId)).Returns(Task.FromResult(new SendResponse("Deleted blog successfully", StatusCodes.Status200OK, actual, "")));
            BlogsController obj = new BlogsController(blogprovider.Object);
            var res = obj.DeleteBlog(actual.BlogId);
            Assert.AreNotEqual(expectedResult, res.Result.ToJson().ToString());


        }

        [Test]
        public void Delete_BlogArgumentsMatching()
        {
            Blog expected = new Blog();
            expected.BlogId = 1;
            expected.BlogTitle = "Blog Title";
            expected.BlogContent = "Blog Content";
            expected.CoverImage = "image.com";
            expected.UserID = 1;
            Blog actual = new Blog();
            actual.BlogId = 1;
            actual.BlogTitle = "Blog Title";
            actual.BlogContent = "Blog Content";
            actual.CoverImage = "image.com";
            actual.UserID = 1;
            SendResponse sendResponse = new SendResponse("Deleted blog successfully", StatusCodes.Status200OK, expected, "");
            string expectedResult = "{\"Value\":" + sendResponse.ToJson().ToString() + ",\"Formatters\":[],\"ContentTypes\":[],\"StatusCode\":200}";
            blogprovider.Setup(x => x.DeleteBlog(actual.BlogId)).Returns(Task.FromResult(new SendResponse("Deleted blog successfully", StatusCodes.Status200OK, actual, "")));
            BlogsController obj = new BlogsController(blogprovider.Object);
            var res = obj.DeleteBlog(actual.BlogId);
            Assert.AreEqual(expectedResult, res.Result.ToJson().ToString());
        }


    }
}

