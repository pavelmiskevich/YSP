using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSP.Core.Models;
using YSP.Data.Configurations.BaseConfigurations;

namespace YSP.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            #region Index
            builder
                .HasIndex(m => m.ParentId);
            #endregion Index
            #region members
            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(m => m.AddDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder
                .Property(m => m.IsActive)
                .HasDefaultValueSql("((1))");
            #endregion members
            #region reference
            builder
                .HasOne(d => d.Parent)
                .WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId);            
            #endregion reference
            
            //builder
            //    .ToTable("Categories");
        }
    }
}
