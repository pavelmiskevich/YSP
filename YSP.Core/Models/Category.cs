using System.Collections.Generic;
using YSP.Core.Models.Base;

namespace YSP.Core.Models
{
    /// <summary>
    /// Категории сайтов, древовичная структура
    /// </summary>
    public class Category : EntityBaseAddDate
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
            Sites = new HashSet<Site>();
        }

        //public int Id { get; set; }
        public string Name { get; set; }
        //public bool IsActive { get; set; }
        public int? ParentId { get; set; }

        //TODO: понять нужеен ли virtual
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> InverseParent { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
    }
}
