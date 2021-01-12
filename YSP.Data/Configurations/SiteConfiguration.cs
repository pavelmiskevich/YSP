using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSP.Core.Models;

namespace YSP.Data.Configurations
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            #region Index
            builder
                .HasIndex(m => m.CategoryId)
                .HasName("Categories_Sites_FK");

            builder
                .HasIndex(m => m.RegionId)
                .HasName("Regions_Sites_FK");

            builder
                .HasIndex(m => m.UserId)
                .HasName("Users_Sites_FK");
            #endregion Index
            #region members

            builder
                .Property(m => m.Descr)
                .HasMaxLength(1000);

            builder
                .Property(m => m.LastCheck)
                .HasColumnType("datetime");

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(m => m.Url)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(m => m.TimeOut)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder
                .Property(m => m.AddDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder
                .Property(m => m.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            #endregion members
            #region reference
            builder
                .HasOne(d => d.Category)
                .WithMany(p => p.Sites)
                .HasForeignKey(d => d.CategoryId);
                //.HasConstraintName("FK_SITES_REFERENCE_CATEGORI");

            builder
                .HasOne(d => d.Region)
                .WithMany(p => p.Sites)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(d => d.User)
                .WithMany(p => p.Sites)
                .HasForeignKey(d => d.UserId);
            #endregion reference
        }
    }
}
