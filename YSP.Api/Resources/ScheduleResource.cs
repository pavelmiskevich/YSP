using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class ScheduleResource : BaseAddDateResorce
    {
        public DateTime Date { get; set; }
        public QueryResource Query { get; set; }
    }
}
