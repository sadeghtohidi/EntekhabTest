using Entekhab.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Domain.Entities
{
    public class SalaryPersonal : BaseEntity<int> , IEntity
    {
        public int EmployeeNumber { get; set; }
        public decimal BasicSalary { get; set; }
        public int YearMonth { get; set; }
        public decimal Allowance { get; set; }
        public decimal Transportation { get; set; }
        public decimal TotalSalary { get; set; } 
        public decimal Tax { get; set; }
        public decimal OverTimeAmount { get; set; }
    }
}
