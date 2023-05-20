using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Dto.Salary
{
    public abstract class BaseSalaryDto
    {
        public int EmployeeNumber { get; set; }
        public decimal BasicSalary { get; set; }
        public int YearMonth { get; set; }
        public decimal Allowance { get; set; }
        public decimal Transportation { get; set; }
        public decimal Tax { get; set; }

    }
}
