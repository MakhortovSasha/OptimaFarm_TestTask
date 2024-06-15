
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimaFarm_TestTask.Models
{
    public class DefaultStats
    {
        private IEnumerable<Emploee> _collection;
        public DefaultStats(IEnumerable<Emploee> emploees) 
        {
            _collection = emploees;
            Recount();
        }

        public void Recount()
        {
            if (_collection == null||_collection.Count()==0){ SetDefaulValues(); return; }
            AvaregeSalary = (_collection.Where(e=>e.IsActive).Select(e => e.Salary).Sum() / _collection.Where(e => e.IsActive).Count());
            MaxSalary = _collection.Where(e=>e.IsActive).Max(e => e.Salary);
            SalaryExpenses = _collection.Where(e => e.IsActive).Sum(e => e.Salary);
            ActiveEmploeeCount = _collection.Where(e => e.IsActive).Count();
            Emploeed_HalfYear = _collection.Where(e => e.EmploymentDate >= DateTime.Now.AddMonths(-6)).Count();
        }
        private void SetDefaulValues()
        {
            AvaregeSalary = MaxSalary = SalaryExpenses =  ActiveEmploeeCount = Emploeed_HalfYear = 0;
        }

        public decimal AvaregeSalary {  get; private set; }
        public decimal MaxSalary { get; private set; }
        public decimal SalaryExpenses { get; private set; }
        public int ActiveEmploeeCount { get; private set; }
        public int Emploeed_HalfYear { get; private set; }

        public override string ToString()
        {

            Recount();
            string returnable = string.Empty;
            returnable += $"Avarage salary: {AvaregeSalary}\n"
                + $"Max salary: {MaxSalary}\n"
                + $"Salary expenses: {SalaryExpenses}\n"
                + $"Current number of employees: {ActiveEmploeeCount}\n"
                + $"Quantity of new employees for the last half a year: {Emploeed_HalfYear}\n";

            return returnable;
        }

        
    }
}
