using System;
using System.Collections.Generic;
using YSP.Core.Models.Base;

namespace YSP.Core.Models
{
    /// <summary>
    /// Сайты для проверки
    /// </summary>
    public class Site : EntityBaseAddDate
    {
        public Site()
        {
            //Competition = new HashSet<Competition>();
            Queries = new HashSet<Query>();
            //Schedule = new HashSet<Schedule>();
            //SitePages = new HashSet<SitePages>();
        }

        //public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        //public DateTime AddDate { get; set; }
#nullable enable
        public string? Descr { get; set; }
#nullable disable
        public DateTime? LastCheck { get; set; }
        /// <summary>
        /// Тайм аут проверки, аналог временной блокировки
        /// </summary>
        public int? TimeOut { get; set; }
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        //TODO: подумать стоит ли выносить регион на уровень запросов!
        public int RegionId { get; set; }
        //public bool IsActive { get; set; }

        //TODO: проверить как работает и работает ли virtual как Lazy
        public virtual Category Category { get; set; }
        public virtual Region Region { get; set; }
        public virtual User User { get; set; }
        //public virtual ICollection<Competition> Competition { get; set; }
        public virtual ICollection<Query> Queries { get; set; }
        //public virtual ICollection<Schedule> Schedule { get; set; }
        //public virtual ICollection<SitePages> SitePages { get; set; }
    }
}
