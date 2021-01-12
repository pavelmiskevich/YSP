using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSP.Core.Models;

namespace YSP.Data.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            #region Index
            builder
                .HasIndex(e => e.QueryId)
                .HasName("Queries_Positions_FK");

            builder
                .HasIndex(e => new { e.QueryId, e.AddDate })
                .HasName("IDX_Positions_QueryId_AddDate");
            #endregion Index
            #region members
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
                .HasOne(d => d.Query)
                .WithMany(p => p.Positions)
                .HasForeignKey(d => d.QueryId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .HasOne(d => d.SitePage)
            //    .WithMany(p => p.Positions)
            //    .HasForeignKey(d => d.SitePageId)
            //    .HasConstraintName("FK_POSITION_REFERENCE_SITEPAGE");
            #endregion reference
        }
    }
}
