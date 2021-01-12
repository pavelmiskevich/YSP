using System;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class SaveUserResource : BaseSaveResource
    {
#nullable enable
        public string? Name { get; set; }
#nullable disable
        public string Email { get; set; }
        //[Encrypted]
        public string Password { get; set; }
#nullable enable
        public string? YandexLogin { get; set; }
        public string? YandexKey { get; set; }
        public string? AvatarLink { get; set; }
        public string? Ip { get; set; }
#nullable disable
        //public bool IsActive { get; set; }
        //public DateTime RegDate { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime LastVisitDate { get; set; }
        public int Limit { get; set; }
        public int FreeLimit { get; set; }
    }
}
