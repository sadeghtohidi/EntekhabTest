using Entekhab.Services.Common.AutoFac;
using Entekhab.Services.Dto.Enum;
using Entekhab.Services.Dto.GetFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Contract
{
    public interface IConvertDataToModelService :IScopedDependency
    {
        Task<ResultConvertData> ConvertDataToModelAsync(string data, EnumDataType dataType, string methodName);
        Task InsertLogApiAsync(string data, bool isSuccess, string message, EnumDataType dataType, string methodName);

    }
}
