using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class SystemStateResource : BaseResource
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
