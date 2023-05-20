using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Dto.GetFile
{
    public class SalaryGetModel
    {
        public int EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal BasicSalary { get; set; }
        public int Date { get; set; }
        public decimal Allowance { get; set; }
        public decimal Transportation { get; set; }
    }
}
