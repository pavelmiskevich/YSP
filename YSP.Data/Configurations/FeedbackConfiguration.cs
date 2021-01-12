using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSP.Core.Models;

namespace YSP.Data.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            #region Index
            builder
                .HasIndex(e => e.UserId)
                .HasName("Users_Feedbacks_FK");
            #endregion Index
            #region members
            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(m => m.AddDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder
                .Property(m => m.IsActive)
                .HasDefaultValueSql("((0))");
            #endregion members|
            #region reference
            builder
                .HasOne(d => d.User)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId);
            #endregion reference
        }
    }
}
