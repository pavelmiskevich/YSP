using System;
using YSP.Core.Models.Base;

namespace YSP.Core.Models
{
    /// <summary>
    /// Расписание проверок позиций сайтов
    /// </summary>
    public class Schedule : EntityBaseAddDate
    {
        //public int Id { get; set; }
        public DateTime Date { get; set; }
        //public int? SiteId { get; set; }
        public int? QueryId { get; set; }
        //TODO: понять зачем планировалось поле Count
        //public int? Count { get; set; }
        //public bool IsActive { get; set; }

        //TODO: понять нужеен ли virtual
        public virtual Query Query { get; set; }
        //public virtual Site Site { get; set; }
    }
}
