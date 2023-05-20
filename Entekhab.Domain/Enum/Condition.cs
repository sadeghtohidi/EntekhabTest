using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Domain.Enum
{
    public enum Condition
    {
        [Display(Name = "در انتظار تایید")]
        AwaitingApproval = 1,
        [Display(Name = "فعال")]
        Active = 2,
        [Display(Name = "غیر فعال")]
        Inactive = 3,
        [Display(Name = "رد شده")]
        Failed = 4,
        [Display(Name = "ناموفق در ثبت نام")]
        FailedRegister = 5,
        [Display(Name = "تایید ادمین")]
        AdminConfirmation = 6
    }
}
