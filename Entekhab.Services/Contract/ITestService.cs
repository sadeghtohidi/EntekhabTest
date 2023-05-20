using Entekhab.Services.Common.AutoFac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Contract
{
    public interface ITestService : IScopedDependency
    {
        void TestMethod();
    }
}
