using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using YSP.Core.Models.Base;

namespace YSP.Data.Configurations.BaseConfigurations
{
    [Obsolete("Класс не используется за непригодностью.", true)]
    public class EntityBaseConfiguration : IEntityTypeConfiguration<EntityBase>
    {
        public void Configure(EntityTypeBuilder<EntityBase> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .UseIdentityColumn();
        }
    }
}
