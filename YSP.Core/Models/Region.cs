using System.Collections.Generic;
using YSP.Core.Models.Base;

namespace YSP.Core.Models
{
    /// <summary>
    /// Регионы проверки поисковой выборки
    /// </summary>
    public class Region : EntityBase
    {
        public Region()
        {
            Sites = new HashSet<Site>();
        }

        public new int Id { get; set; }
        public string Name { get; set; }
        //public bool IsActive { get; set; }

        //TODO: понять нужеен ли virtual
        public virtual ICollection<Site> Sites { get; set; }
    }
}
