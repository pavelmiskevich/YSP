using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSP.Core.Models;

namespace YSP.Data.Configurations
{
    public class SystemStateConfiguration : IEntityTypeConfiguration<SystemState>
    {
        public void Configure(EntityTypeBuilder<SystemState> builder)
        {
            #region members
            builder
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.AddDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder
                .Property(m => m.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            #endregion members;
        }
    }
}
