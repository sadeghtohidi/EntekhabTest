using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entekhab.Services.Contract;
using Entekhab.Services.Dto.GetFile;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NUnit.Framework;


namespace Entekhab.Test
{
    [TestFixture]
    public class TestCalculator
    {
        private IConvertDataToModelService _convertDataToModelService;
        [OneTimeSetUp]
        public void SetUp(IConvertDataToModelService convertDataToModelService)
        {
            _convertDataToModelService = convertDataToModelService;  
        }


        [Test]
        public async Task IsCurrentMethod()
        {
            var model = new List<SalaryGetModel>();

            var result = await _convertDataToModelService.ConvertDataToModelAsync("", Services.Dto.Enum.EnumDataType.Json, "CalCulatorA");
            Assert.IsNotNull(result);
            
        }
    }
}
