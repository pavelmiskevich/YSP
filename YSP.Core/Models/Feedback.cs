using YSP.Core.Models.Base;

namespace YSP.Core.Models
{
    /// <summary>
    /// Отзывы пользователей
    /// </summary>
    public class Feedback : EntityBaseAddDate
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        //public DateTime CreDate { get; set; }
        public int? UserId { get; set; }
        //public bool IsActive { get; set; }

        //TODO: понять нужеен ли virtual
        public virtual User User { get; set; }
    }
}
