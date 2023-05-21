using AdminSalary.DataLayer.Repositories.Dapper;
using Microsoft.AspNetCore.Mvc;
using Entekhab.Domain.Entities;
using Entekhab.Services.Contract;
using Entekhab.Services.Dto.Enum;
using Entekhab.Services.Dto.GetFile;
using Entekhab.Services.Dto.Salary;
using WebFramework.Api;
using WebFramework.Filters;

namespace Entekhab.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ApiResultFilter]
    public class HomeController : Controller
    {
        private readonly Lazy<IConvertDataToModelService> _convertDataToModelService;
        private readonly Lazy<ISalaryService> _salaryService;
        private readonly Lazy<IDapperRepositoryTEntity<SalaryPersonal>> _dapperRepository;
        public HomeController(Lazy<IConvertDataToModelService> convertDataToModelService , Lazy<ISalaryService> salaryService
            , Lazy<IDapperRepositoryTEntity<SalaryPersonal>> dapperRepository)
        {
            
            _convertDataToModelService = convertDataToModelService;
            _salaryService = salaryService;
            _dapperRepository = dapperRepository;
        }

        [HttpGet("{datatype}/[controller]/[action]")]
        public async Task<ApiResult> GetFile(string datatype, [FromBody] GetFileRequestDto requestDto)
        {
            var type = EnumDataType.Json;
            switch (datatype.ToUpper())
            {
                case "JSON":
                    type = EnumDataType.Json;
                    break;

                case "XML":
                    type = EnumDataType.Xml;
                    break;

                case "CS":
                    type = EnumDataType.Csv;
                    break;

                case "CUSTOM":
                    type = EnumDataType.Custom;
                    break;

                default:
                    var result = new ApiResult(false, Entekhab.Common.ApiResult.ApiResultStatusCode.ProcessError, "نوع فایل نادرست است");
                    await _convertDataToModelService.Value.InsertLogApiAsync(requestDto.Data, result.IsSuccess,result.Message, type, requestDto.OverTimeCalculator);
                    return result;
                    
            }
            var resultConvert = await _convertDataToModelService.Value.ConvertDataToModelAsync(requestDto.Data, type, requestDto.OverTimeCalculator);
            if (resultConvert.IsSuccess)
            {
                var result = new ApiResult(true, Entekhab.Common.ApiResult.ApiResultStatusCode.Success, "");
                await _convertDataToModelService.Value.InsertLogApiAsync(requestDto.Data, result.IsSuccess, result.Message, type, requestDto.OverTimeCalculator);
                return result;
            }
            else
            {
                var result = new ApiResult(false, Entekhab.Common.ApiResult.ApiResultStatusCode.ProcessError, resultConvert.Message);
                await _convertDataToModelService.Value.InsertLogApiAsync(requestDto.Data, result.IsSuccess, result.Message, type, requestDto.OverTimeCalculator);
                return result;
            }

        }


        [HttpPost("Add")]
        public async Task<ApiResult> Add([FromQuery]  AddUpdateSalaryRequestDto requestDto)
        {
            await _salaryService.Value.Add_UpdateAsync(requestDto);
            return Ok();
        }
        [HttpPut("Update")]
        public async Task<ApiResult> Update([FromQuery] AddUpdateSalaryRequestDto requestDto)
        {
            await _salaryService.Value.Add_UpdateAsync(requestDto);
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete([FromQuery] DeleteSalaryRequestDto requestDto)
        {
            await _salaryService.Value.DeleteAsync(requestDto);
            return Ok();
        }

        [HttpGet("Get")]
        public async Task<SalaryPersonal> Get([FromQuery]  GetSalaryRequestDto requestDto)
        {
            var parametrs = new { EmployeeNumber = requestDto.EmployeeNumber, YearMonth = requestDto.YearMonth };
            var result = await _dapperRepository.Value.GetAsync(" Where EmployeeNumber = @EmployeeNumber AND YearMonth = @YearMonth", parametrs);

            return result;
        }
        [HttpGet("GetRange")]
        public async Task<List<SalaryPersonal>> GetRange([FromQuery]  GetRangeSalaryRequestDto requestDto)
        {

            var parametrs = new { EmployeeNumber = requestDto.EmployeeNumber, StartYearMonth = requestDto.StartYearMonth , EndYearMonth = requestDto.EndYearMonth};
            var result = await _dapperRepository.Value.GetRangeAsync(" Where EmployeeNumber = @EmployeeNumber AND YearMonth >= @StartYearMonth AND YearMonth <= @EndYearMonth",parametrs);

            return result;
        }

    }
}
