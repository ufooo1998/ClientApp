using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Entities
{
    class Student
    {
        public int accountId { get; set; }
        public account account { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTimeOffset bod { get; set; }
        public int gender { get; set; }
        public string avatar { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string description { get; set; }
    }

    public class account
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string deletedAt { get; set; }
        public int status { get; set; }
        public gradeStudents[] gradeStudents { get; set; }
        public string marks { get; set; }
        public string accountRoles { get; set; }
    }

    public class gradeStudents
    {
        public int id { get; set; }
        public int gradeId { get; set; }
        public grade grade { get; set; }
        public string accountId { get; set; }
        public string joinAt { get; set; }

    }

    public class grade
    {
        public int id { get; set; }
        public string gradeName { get; set; }
        public string startDate { get; set; }
        //public Dictionary<string, gradeStudents> gradeStudents { get; set; }
        public string subjectGrades { get; set; }
        public int status { get; set; }

    }
}
