using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class CategoryResource : BaseAddDateResorce
    {        
        public string Name { get; set; }
        public CategoryResource Parent { get; set; }
    }
}
