using System;
namespace CourseSelectionAPI.Models
{
    public class User
    {
        public int id { set; get; }

        public string username { set; get; }

        public string password { set; get; }

        public string email { set; get; }

        public string firstname { set; get; }

        public string lastname { set; get; }

        public DateTime created_at { set; get; }

    }
}
