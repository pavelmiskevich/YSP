using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSP.Core.Models;

namespace YSP.Data.Configurations
{
    public class QueryConfiguration : IEntityTypeConfiguration<Query>
    {
        public void Configure(EntityTypeBuilder<Query> builder)
        {
            #region Index
            //builder
            //    .HasIndex(e => e.SeoId)
            //    .HasName("Seo_Queries_FK");

            builder
                .HasIndex(e => e.SiteId)
                .HasName("Sites_Queries_FK");

            builder
                .HasIndex(e => new { e.Id, e.IsActive }) //, e.SiteId
                .HasName("IDX_Queries_Id_IsActive"); //_SiteId
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
                .IsRequired()
                .HasDefaultValueSql("((1))");
            #endregion members
            #region reference
            //builder
            //    .HasOne(d => d.Seo)
            //    .WithMany(p => p.Queryes)
            //    .HasForeignKey(d => d.SeoId)
            //    .HasConstraintName("FK_QUERYES_REFERENCE_SEO");

            builder
                .HasOne(d => d.Site)
                .WithMany(p => p.Queries)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion reference
        }
    }
}
