using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Domain.Enum
{
    public enum Gender
    {
        [Display(Name = "زن")]
        Female = 0,

        [Display(Name = "مرد")]
        Male = 1,
    }
}
