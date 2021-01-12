using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class SaveFeedbackResource : BaseSaveResource
    {
        public string Name { get; set; }
        public int? UserId { get; set; }
    }
}
