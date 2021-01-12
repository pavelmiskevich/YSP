using System;
using System.Collections.Generic;
using System.Text;
using YSP.Core.DTO.Base;
using YSP.Core.Models;

namespace YSP.Core.DTO
{
    public class QueryRegionDTO : BaseDTO
    {
        /// <summary>
        /// Id Schedule for deactivate
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// Query Name
        /// </summary>
        public string QueryName { get; set; }
        /// <summary>
        /// Site region
        /// </summary>
        public Region Region { get; set; }
        /// <summary>
        /// Site url
        /// </summary>
        public string Url { get; set; }        
    }
}
