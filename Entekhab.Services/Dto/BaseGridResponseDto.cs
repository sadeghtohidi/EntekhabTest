using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Dto
{
    public abstract class BaseGridResponseDto<T>
    {
        public int TotalRecord { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
