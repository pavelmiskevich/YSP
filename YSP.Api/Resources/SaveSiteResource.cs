using System;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class SaveScheduleResource : BaseSaveResource
    {
        public DateTime? Date { get; set; }
        public int QueryId { get; set; }
    }
}
