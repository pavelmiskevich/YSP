using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using YSP.Core.Models.Base;

namespace YSP.Data.Configurations.BaseConfigurations
{
    [Obsolete("Класс не используется за непригодностью.", true)]
    public class EntityBaseAddDateConfiguration : IEntityTypeConfiguration<EntityBaseAddDate>
    {
        public void Configure(EntityTypeBuilder<EntityBaseAddDate> builder)
        {
            builder
                .Property(m => m.AddDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
        }
    }
}
