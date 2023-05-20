using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Dto.Salary
{
    public class GetRangeSalaryRequestDto
    {
        public int EmployeeNumber { get; set; }
        public int StartYearMonth { get; set; }
        public int EndYearMonth { get; set; }

    }
}
