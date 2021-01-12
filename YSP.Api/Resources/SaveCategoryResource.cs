using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class SaveCategoryResource : BaseSaveResource
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
