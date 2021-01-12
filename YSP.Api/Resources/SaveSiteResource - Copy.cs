using System;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class SaveSiteResource : BaseSaveResource
    {
        public string Name { get; set; }
        public string Url { get; set; }
#nullable enable
        public string? Descr { get; set; }
#nullable disable
        public DateTime? LastCheck { get; set; }
        public int? TimeOut { get; set; }
        public int CategoryId { get; set; }
        public int RegionId { get; set; }
        public int UserId { get; set; }
    }
}
