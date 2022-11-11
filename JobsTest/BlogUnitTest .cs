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
    public class BlogUnitTest
    {

        //Arrange
        List<Blog> Blogs = new List<Blog>();
        IQueryable<Blog> blogdata;
        Mock<DbSet<Blog>> mockSet;
        Mock<userDbContext> blogcontextmock;
        Mock<IBlogsRepo> blogprovider;

        [SetUp]
        public void Setup()
        {
            blogdata = Blogs.AsQueryable();
            mockSet = new Mock<DbSet<Blog>>();
            mockSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(blogdata.Provider);
            mockSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(blogdata.Expression);
            mockSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(blogdata.ElementType);
            mockSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(blogdata.GetEnumerator());
            var p = new DbContextOptions<userDbContext>();
            blogcontextmock = new Mock<userDbContext>(p);
            blogprovider = new Mock<IBlogsRepo>();

        }

        [Test]
        public void DeleteBlog_TypeMatching()
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

