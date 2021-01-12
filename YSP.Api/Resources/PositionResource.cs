using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class PositionResource : BaseAddDateResorce
    {        
        public int Pos { get; set; }
        public QueryResource Query { get; set; }
    }
}
