using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSP.Api.Resources.Base;

namespace YSP.Api.Resources
{
    public class UserResource : BaseAddDateResorce
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
