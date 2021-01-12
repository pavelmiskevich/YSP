using System;
using System.Collections.Generic;
using YSP.Core.Models.Base;

namespace YSP.Core.Models
{
    /// <summary>
    /// Запросы для проверки видимости Яндексом
    /// </summary>
    public class Query : EntityBaseAddDate
    {
        public Query()
        {
            //Competition = new HashSet<Competition>();
            //Errors = new HashSet<Errors>();
            Positions = new HashSet<Position>();
            Schedules = new HashSet<Schedule>();
        }

        //public int Id { get; set; }
        public string Name { get; set; }
        //public DateTime AddDate { get; set; }
        public DateTime? LastCheck { get; set; }
        //public bool IsActive { get; set; }
        public int SiteId { get; set; }
        //TODO: поле для расширения информативности
        //public int? SeoId { get; set; }

        //TODO: понять нужеен ли virtual
        //public virtual Seo Seo { get; set; }
        public virtual Site Site { get; set; }
        //public virtual ICollection<Competition> Competition { get; set; }
        //public virtual ICollection<Errors> Errors { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }        
    }
}
