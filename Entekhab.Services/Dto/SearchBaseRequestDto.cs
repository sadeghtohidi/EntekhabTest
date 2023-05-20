using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Dto
{
    public abstract class SearchBaseRequestDto
    {
        public int SkipRecord { get { return ((PageNumber == 0 ? 1 : PageNumber) - 1) * PageSize; } }
        public int PageSize { get; set; }
        public string? SortField { get; set; }
        public bool SortAsc { get; set; }
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
    }

}
