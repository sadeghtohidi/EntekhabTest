using Entekhab.Services.Common.AutoFac;
using Entekhab.Services.Dto.GetFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Contract
{
    public interface IOvetimePolicies : IScopedDependency
    {
        public void CalcurlatorA(List<SalaryGetModel> request);
        public void CalcurlatorB(List<SalaryGetModel> request);
        public void CalcurlatorC(List<SalaryGetModel> request);
    }
}
