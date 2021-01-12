using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YSP.Core.Models.Base;

namespace YSP.Core.Models
{
    /// <summary>
    /// Пользователи и владельцы сайтов
    /// </summary>
    public class User : EntityBaseAddDate
    {
        public User()
        {
            Feedbacks = new HashSet<Feedback>();
            Offers = new HashSet<Offer>();
            Sites = new HashSet<Site>();
            //Useful = new HashSet<Useful>();
        }

        //public int Id { get; set; }
#nullable enable
        public string? Name { get; set; }
        public string Email { get; set; }
        [Encrypted]
        public string Password { get; set; }
        public string? YandexLogin { get; set; }
        public string? YandexKey { get; set; }
        public string? GoogleCx { get; set; }
        public string? GoogleKey { get; set; }
        public string? AvatarLink { get; set; }
        public string? Ip { get; set; }
#nullable disable
        //public bool IsActive { get; set; }
        //public DateTime RegDate { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime LastVisitDate { get; set; }
        public int YandexLimit { get; set; }
        public int GoogleLimit { get; set; }
        public int FreeLimit { get; set; }
        //public string WebmasterId { get; set; }

        //TODO: понять нужеен ли virtual
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        //public virtual ICollection<Useful> Useful { get; set; }
    }
}
