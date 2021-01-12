using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class FeedbackResource : BaseAddDateResorce
    {        
        public string Name { get; set; }
#nullable enable
        public UserResource? User { get; set; }
#nullable disable
    }
}
