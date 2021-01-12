using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class QueryResource : BaseAddDateResorce
    {
        public string Name { get; set; }
        public DateTime? LastCheck { get; set; }
        public SiteResource Site { get; set; }
    }
}
