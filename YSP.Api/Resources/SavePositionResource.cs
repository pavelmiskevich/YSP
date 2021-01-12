using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class SavePositionResource : BaseSaveResource
    {
        public int Pos { get; set; }
        public int QueryId { get; set; }
    }
}
