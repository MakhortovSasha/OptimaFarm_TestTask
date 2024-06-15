using OptimaFarm_TestTask.Models;
using System;

namespace UnitTests
{
    public class Test
    {

        public Test()
        {
            Test1 = "Test";
            Test2 = 10;
        }
        public string Test1 { get; set; }
        public int Test2 { get; set; }
    }

    public class Creator
    {
        public static Emploee CreateTestEmploee()
        {
            return new Emploee()
            {
                Birthday = DateTime.MinValue,
                City = "1",
                CorporatePhoneNumber = "1",
                Department = "1",
                EmploymentDate = DateTime.MinValue,
                FirstName = "2",
                INN = "1",
                IsActive = true,
                LastName = "1",
                PersonalPhoneNumber = "3",
                Position = "1",
                Salary = 100,
                SecondName = "1",
                _id = 1
            };

        }
    }

}
