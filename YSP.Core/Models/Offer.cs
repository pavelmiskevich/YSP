using System;
using YSP.Core.Models.Base;

namespace YSP.Core.Models
{
    /// <summary>
    /// Предложения пользователей и их выполнение
    /// </summary>
    public class Offer : EntityBaseAddDate
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        //public int? PageId { get; set; }
        public int? UserId { get; set; }
        //public DateTime CreDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        //public bool IsActive { get; set; }

        //TODO: понять нужеен ли virtual
        //public virtual Pages Page { get; set; }
        public virtual User User { get; set; }
    }
}
