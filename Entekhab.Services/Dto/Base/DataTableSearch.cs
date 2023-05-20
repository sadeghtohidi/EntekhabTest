using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Services.Dto.Base
{
    public enum SortType { asc, desc }
    public class DataTableSearch
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Search { get; set; }
        public List<DataTableSort> Sort { get; set; }
        public List<DataTableFilter> Filters { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int TotalData { get; set; }
        public string TotalReserve { get; set; }
        public int? Id { get; set; }
        public string userid { get; set; }
        public string sortField { get; set; }
        public bool sortasc { get; set; }
        public bool onlyLastConfig { get; set; }
    }
    public class DataTableSort
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public SortType SortType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Type) || Type == "asc")

                {
                    return SortType.asc;
                }
                else
                    return SortType.desc;
            }
        }
    }
    public class DataTableFilter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
