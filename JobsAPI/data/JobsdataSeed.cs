using JobsAPI.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace JobsAPI.data
{
    public class JobsdataSeed
    {
        public static async Task SeedAsync(userDbContext context, ILoggerFactory logerFactory)
        {
            try
            {
                // categ
                if (!context.Categories.Any())
                {
                    var categoryData  =
                        File.ReadAllText("./data/SeedData/category.json");
                    var categoryList= JsonSerializer.Deserialize<List<Category>>(categoryData);
                    foreach (var item in categoryList)
                    {
                        context.Categories.Add(item);
                    }

                    await context.SaveChangesAsync();

                }

                if (!context.States.Any())
                {
                    var statesData =
                        File.ReadAllText("./data/SeedData/state.json");
                    var statesList = JsonSerializer.Deserialize<List<State>>(statesData);
                    foreach (var item in statesList)
                    {
                        context.States.Add(item);
                    }

                    await context.SaveChangesAsync();

                }

                if (!context.Cities.Any())
                {
                    var citiesData =
                        File.ReadAllText("./data/SeedData/city.json");
                    var citiesList = JsonSerializer.Deserialize<List<City>>(citiesData);
                    foreach (var item in citiesList)
                    {
                        context.Cities.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Users.Any())
                {
                    var adminData = File.ReadAllText("./data/SeedData/adminlogin.json");
                    var admindetails = JsonSerializer.Deserialize<List<user>>(adminData);
                    foreach (var item in admindetails)
                    {
                        context.Users.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Jobs.Any())
                {
                    var jobsData =
                        File.ReadAllText("./data/SeedData/jobsdata.json");
                    var jobsList = JsonSerializer.Deserialize<List<Job>>(jobsData);
                    foreach (var item in jobsList)
                    {
                        context.Jobs.Add(item);
                    }

                    await context.SaveChangesAsync();

                }

               

                if (!context.Courses.Any())
                {
                    var courseData =
                        File.ReadAllText("./data/SeedData/courses.json");
                    var courseList = JsonSerializer.Deserialize<List<Course>>(courseData);
                    foreach (var item in courseList)
                    {
                        context.Courses.Add(item);
                    }

                    await context.SaveChangesAsync();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //var logger = LoggerFactory.CreateLogger<userDbContext>();
                //logger.LogError(ex.Message);
            }
        }

    }
}
