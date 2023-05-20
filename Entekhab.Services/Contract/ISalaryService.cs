using Entekhab.Services.Common.AutoFac;
using Entekhab.Services.Dto.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Contract
{
    public interface ISalaryService : IScopedDependency
    {
        Task Add_UpdateAsync(AddUpdateSalaryRequestDto requestDto);
        Task DeleteAsync(DeleteSalaryRequestDto requestDto);
    }
}
