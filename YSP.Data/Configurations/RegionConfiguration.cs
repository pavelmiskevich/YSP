using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSP.Core.Models;

namespace YSP.Data.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            #region members
            builder
                .Property(m => m.Id).ValueGeneratedNever();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            #endregion members
        }
    }
}
