using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class SiteResource : BaseAddDateResorce
    {
        public string Name { get; set; }
        public string Url { get; set; }
#nullable enable
        public string? Descr { get; set; }
#nullable disable
        public DateTime? LastCheck { get; set; }
        public int? TimeOut { get; set; }
        public CategoryResource Category { get; set; }
        public RegionResource Region { get; set; }
        public UserResource User { get; set; }
    }
}
