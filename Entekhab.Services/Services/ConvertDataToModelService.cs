using Data.Repositories;
using Helpers;
using Newtonsoft.Json;
using Entekhab.Common;
using Entekhab.Domain.Entities;
using Entekhab.Services.Contract;
using Entekhab.Services.Dto.Enum;
using Entekhab.Services.Dto.GetFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Services
{
    public class ConvertDataToModelService : IConvertDataToModelService
    {
        private readonly IOvetimePolicies _ovetimePolicies;
        private readonly IRepository<LogUploadFile> _logUploadFileRepository;

        public ConvertDataToModelService(IOvetimePolicies ovetimePolicies, IRepository<LogUploadFile> logUploadFileRepository)
        {
            _ovetimePolicies = ovetimePolicies;
            _logUploadFileRepository = logUploadFileRepository;
        }

        public async Task<ResultConvertData> ConvertDataToModelAsync(string data, EnumDataType dataType, string methodName)
        {
            var result = new ResultConvertData();


            switch (dataType)
            {
                case EnumDataType.Json:
                    result = await ConvertJsonToModel(data);
                    break;
                case EnumDataType.Xml:
                    result = await ConvertXMLToModel(data);
                    break;
                case EnumDataType.Csv:
                    result = await ConvertCsToModel(data);
                    break;
                case EnumDataType.Custom:
                    result = await ConvertCustomToModel(data);
                    break;
                default:
                    result.IsSuccess = false;
                    result.Message = "نوع فایل نادرست بوده است";
                    return result;
            }
            if (result.IsSuccess)
            {
                switch (methodName.ToUpper())
                {
                    case "CALCURLATORA":
                        _ovetimePolicies.CalcurlatorA(result.Data);
                        break;
                    case "CALCURLATORB":
                        _ovetimePolicies.CalcurlatorB(result.Data);
                        break;
                    case "CALCURLATORC":
                        _ovetimePolicies.CalcurlatorC(result.Data);
                        break;

                    default:
                        result.IsSuccess = false;
                        result.Message = "نام متد نادرست بوده است";
                        return result;
                }
            }
            return result;
        }

        private async Task<ResultConvertData> ConvertJsonToModel(string data)
        {
            var result = new ResultConvertData();
            if (string.IsNullOrWhiteSpace(data))
            {
                result.IsSuccess = false;
                result.Message = "محتوا خالی است";
                return result;
            }
            else
            {
                try
                {
                    var model = new List<SalaryGetModel>();
                    model = JsonConvert.DeserializeObject<List<SalaryGetModel>>(data);
                    result.IsSuccess = true;
                    result.Message = "عملیان با موفقیت انجام شد";
                    result.Data = model;
                    return result;
                }
                catch (Exception ex)
                {

                    result.IsSuccess = false;
                    result.Message = "عملیات با خطا مواجه شد." + ex.Message;
                    return result;
                }

            }

        }

        private async Task<ResultConvertData> ConvertXMLToModel(string data)
        {
            var result = new ResultConvertData();
            if (string.IsNullOrWhiteSpace(data))
            {
                result.IsSuccess = false;
                result.Message = "محتوا خالی است";
                return result;
            }
            else
            {
                var model = new List<SalaryGetModel>();
                model = ConvertXmlToModel.ParseXML<List<SalaryGetModel>>(data);
                result.IsSuccess = true;
                result.Message = "عملیان با موفقیت انجام شد";
                result.Data = model;
                return result;
            }

        }

        private async Task<ResultConvertData> ConvertCsToModel(string data)
        {
            var result = new ResultConvertData();
            if (string.IsNullOrWhiteSpace(data))
            {

                result.IsSuccess = false;
                result.Message = "محتوا خالی است";
                return result;
            }
            else
            {
                var convert = ConvertTextToModel(data, ",",hasHeader:true);
                if (convert.Item2)
                {
                    result.IsSuccess = true;
                    result.Message = "عملیان با موفقیت انجام شد";
                    result.Data = convert.Item1;
                    return result;
                }
                else
                {

                    result.IsSuccess = false;
                    result.Message = convert.Item3;
                    return result;
                }
            }

        }
        private async Task<ResultConvertData> ConvertCustomToModel(string data)
        {
            var result = new ResultConvertData();
            if (string.IsNullOrWhiteSpace(data))
            {
                result.IsSuccess = false;
                result.Message = "محتوا خالی است";
                return result;
            }
            else
            {
                var convert = ConvertTextToModel(data, "/", hasHeader: true);
                if (convert.Item2)
                {
                    result.IsSuccess = true;
                    result.Message = "عملیان با موفقیت انجام شد";
                    result.Data = convert.Item1;
                    return result;
                }
                else
                {

                    result.IsSuccess = false;
                    result.Message = convert.Item3;
                    return result;
                }

            }

        }

        private Tuple<List<SalaryGetModel>, bool, string> ConvertTextToModel(string data, string seperator, bool hasHeader = true)
        {
            var model = new List<SalaryGetModel>();
            int i = 0;
            string message = "";
            try
            {


                foreach (var myString in data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    i++;
                    if (i == 1 || hasHeader)  // Break Header
                        break;

                    string[] values = data.Split(seperator);
                    if (values.Count() == 6)
                    {
                        var record = new SalaryGetModel()
                        {
                            FirstName = values[0],
                            LastName = values[1],
                            BasicSalary = Convert.ToDecimal(values[2]),
                            Allowance = Convert.ToDecimal(values[3]),
                            Transportation = Convert.ToDecimal(values[4]),
                            Date = Convert.ToInt32(values[5]),
                        };
                        model.Add(record);
                    }
                    else
                    {
                        message = " خط" + i.ToString() + " دارای خطا میباشد";
                        return Tuple.Create(model, false, message);
                    }
                }

                return Tuple.Create(model, true, message);
            }
            catch (Exception ex)
            {
                return Tuple.Create(model, false, "عملیات تبدیل  در خط" + i.ToString() + " با خطا مواجه شد ");

            }

        }

        public async Task InsertLogApiAsync(string data,bool isSuccess , string message , EnumDataType dataType, string methodName)
        {
            await _logUploadFileRepository.AddAsync(new LogUploadFile()
            {
                Created = DateTime.Now,
                Data = data,
                Description = message,
                Result = isSuccess,
                TypeFile = dataType.ToString(),
                MethodName = methodName,
            });
        }
    }
}
