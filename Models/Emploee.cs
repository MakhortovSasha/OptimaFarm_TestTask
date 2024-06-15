using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OptimaFarm_TestTask.Models
{
    public class Emploee : IDataErrorInfo
    {
        public Emploee()
        {

        }
        public Emploee(Emploee e)
        {
            _id = e._id;
            FirstName = e.FirstName;
            LastName = e.LastName;
            SecondName = e.SecondName;
            INN = e.INN;
            PersonalPhoneNumber = e.PersonalPhoneNumber;
            CorporatePhoneNumber = e.CorporatePhoneNumber;
            Department = e.Department;
            Position = e.Position;
            City = e.City;
            Salary = e.Salary;
            IsActive = e.IsActive;
            Birthday = e.Birthday;
            EmploymentDate = e.EmploymentDate;
        }

        public bool Validate()
        {
            foreach (var fn in typeof(Emploee).GetProperties())
            {
                if (this[fn.Name].Length > 0) { return false; }
            }

            return true;
        }

        public bool Contains(string key)
        {

            if (string.IsNullOrEmpty(key)) return true;
            if (string.IsNullOrWhiteSpace(key)) return true;
            var keys = key.Split(' ');
            foreach (var k in keys)
            {

                if (_id.ToString().Contains(k) || SecondName.ToLower().Contains(k.ToLower()) || FirstName.ToLower().Contains(k.ToLower()) || LastName.ToLower().Contains(k.ToLower()))
                    continue;
                else return false;

            }
            return true;
        }

        public int _id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string INN { get; set; }
        public string PersonalPhoneNumber { get; set; }
        public string CorporatePhoneNumber { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string City { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        public string Error { get { return string.Empty; } }

        public string this[string columnName]
        {
            get
            {
                Regex pnRegex = new Regex(@"^\+[1-9]\d{3}-\d{3}-\d{4}$");
                string error = String.Empty;
                switch (columnName)
                {

                    case "FirstName":
                        if (FirstName == null || FirstName.Trim().Length == 0)
                            error = "Last name not valid";
                        break;
                    case "SecondName":
                        if (SecondName == null || SecondName.Trim().Length == 0)
                            error = "Last name not valid";
                        break;
                    case "LastName":
                        if (LastName == null || LastName.Trim().Length == 0)
                            error = "Last name not valid";
                        break;
                    case "Birthday":
                        if (Birthday.AddYears(16)>= EmploymentDate)
                        {
                            error = "An employee cannot be under 16 years of age at the time of hire";
                        }
                        break;
                    /*case "PersonalPhoneNumber":
                        if (string.IsNullOrEmpty(PersonalPhoneNumber))
                        {
                            error = "Invalid phone number"; break;
                        }
                        MatchCollection mc = pnRegex.Matches(PersonalPhoneNumber);
                        if (mc.Count < 1) 
                        {
                            error = "Invalid phone number";
                        }
                        break;*/
                    case "Department":
                        if (Department == null || Department.Trim().Length == 0)
                            error = "Last name not valid";
                        break;
                    case "Position":
                        if (Position == null || Position.Trim().Length == 0)
                            error = "Last name not valid";
                        break;
                    case "City":
                        if (City == null || City.Trim().Length == 0)
                            error = "Last name not valid";
                        break;
                        default:
                        break;

                }
                return error;
            }
        }

        public  bool CustomEquals(object obj)
        {
            if (obj == null || !(obj is Emploee)) return false;

            foreach (var propertyInfo in typeof(Emploee).GetProperties().Where(p => p.Name != "Item"))
                if (!propertyInfo.GetValue(obj).Equals(propertyInfo.GetValue(this)))
                    return false;

            return true;
        }
    }
}
