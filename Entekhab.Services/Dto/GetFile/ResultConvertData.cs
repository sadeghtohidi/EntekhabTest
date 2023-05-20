using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Dto.GetFile
{
    public class ResultConvertData
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<SalaryGetModel> Data { get; set; }
    }
}
