using Microsoft.EntityFrameworkCore.Query.Internal;
using Entekhab.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entekhab.Domain.Entities
{
    public class LogUploadFile : BaseEntity<int>,IEntity
    {
        public DateTime Created { get; set; }
        public string TypeFile { get; set; }
        public bool Result { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public string MethodName { get; set; }
    }
}
