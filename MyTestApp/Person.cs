using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MyTestApp
{
    public class Person
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        public int Age { get; set; }

        public Person()
        {

        }

        public Person(string gender, string firstChar)
        {
            FullName = firstChar + "ivanov";
            DateOfBirth = "10.10.2000";
            Gender = gender;
            Age = 22;
        }

        public Person (string fullName, string dateOfBirth, string gender)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Age = CalculateAge(dateOfBirth);
        }

        private int CalculateAge(string dateOfBirth)
        {
            int age;
            var date = DateTime.ParseExact(dateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            age = DateTime.Now.Year - date.Year;
            if (DateTime.Now.Month < date.Month || (DateTime.Now.Month == date.Month && DateTime.Now.Day < date.Day))
            {
                age--;
            }
            return age;
        }
    }
}
