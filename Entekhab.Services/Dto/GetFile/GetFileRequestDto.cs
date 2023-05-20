using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Dto.GetFile
{
  
    public abstract class GetFileRequestDto
    {
        [Required(ErrorMessage = "(*)")]
        [Display(Name = "سال و ماه حقوق")]
        public int SalaryYearMonth { get; set; }

        [Required(ErrorMessage = "(*)")]
        public string OverTimeCalculator { get; set; }

        [Required(ErrorMessage = "(*)")]
        public string Data { get; set; }
    }
}
