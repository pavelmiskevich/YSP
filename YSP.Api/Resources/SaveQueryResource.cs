using System;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class SaveQueryResource : BaseSaveResource
    {
        public string Name { get; set; }
        public DateTime? LastCheck { get; set; }
        public int SiteId { get; set; }
    }
}
