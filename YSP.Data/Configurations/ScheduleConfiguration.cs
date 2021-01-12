using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSP.Core.Models;

namespace YSP.Data.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            #region Index
            builder
                .HasIndex(m => m.QueryId)
                .HasName("Queries_Schedule_FK");

            //builder
            //    .HasIndex(m => m.SiteId)
            //    .HasName("Sites_Schedule_FK");
            #endregion Index
            #region members
            builder
                .Property(m => m.Date)
                .HasColumnType("date")
                .HasDefaultValueSql("(CONVERT([varchar](8),getdate(),(112)))");

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
                .WithMany(p => p.Schedules)
                .HasForeignKey(d => d.QueryId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder
            //    .HasOne(d => d.Site)
            //    .WithMany(p => p.Schedule)
            //    .HasForeignKey(d => d.SiteId)
            //    .HasConstraintName("FK_SCHEDULE_REFERENCE_SITES");
            #endregion reference
        }
    }
}
