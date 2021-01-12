using YSP.Core.Models.Base;

namespace YSP.Core.Models
{
    /// <summary>
    /// Позиции сайтов
    /// </summary>
    public class Position : EntityBaseAddDate
    {
        //public int Id { get; set; }
        public int Pos { get; set; }
        //public DateTime CheckDate { get; set; }
        //public bool? IsActive { get; set; }
        //TODO: поле для расширения информативности
        //public int? SitePageId { get; set; }
        public int QueryId { get; set; }

        //TODO: понять нужеен ли virtual
        public virtual Query Query { get; set; }
        //public virtual SitePages SitePage { get; set; }
    }
}
