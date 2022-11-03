using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsAPI.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string description { get; set; }
        public string redirect_url { get; set; }
        public decimal salary_max { get; set; }
        public string location { get; set; }


        public string title { get; set; }
        public decimal salary_min { get; set; }
        public string company { get; set; }
        public string created { get; set; }

        //[ForeignKey("State")] public virtual int state { get; set; }
        //[ForeignKey("City")] public virtual int city { get; set; }
        //[ForeignKey("Category")] public virtual int category { get; set; }
        //[ForeignKey("user")] public virtual int userType { get; set; }


        public State state { get; set; }
        public int stateid { get; set; }
        public City city { get; set; }
        public int cityid { get; set; }
        public Category category { get; set; }
        public int categoryid { get; set; }

        public user? userType { get; set; }
    }
}
