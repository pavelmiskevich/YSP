using System;
using YSP.Core.Models.Base;

namespace YSP.Core.Models
{
    /// <summary>
    /// Системная статистика
    /// </summary>
    public class SystemState : EntityBaseAddDate
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        //public DateTime Date { get; set; }
    }
}
