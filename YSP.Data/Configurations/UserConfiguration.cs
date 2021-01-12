using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSP.Core.Models;

namespace YSP.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region members
            builder
                .Property(m => m.Name)
                .HasMaxLength(250);

            builder
                .Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(m => m.Password)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(m => m.YandexLogin)
                .HasMaxLength(250);

            builder
                .Property(m => m.YandexKey)
                .HasMaxLength(250);

            builder
                .Property(m => m.GoogleCx)
                .HasMaxLength(250);

            builder
                .Property(m => m.GoogleKey)
                .HasMaxLength(250);

            builder
                  .Property(m => m.AvatarLink)
                  .HasMaxLength(250);

            builder
                  .Property(m => m.Ip)
                  .HasMaxLength(20);

            builder
                  .Property(m => m.Birthday)
                  .HasColumnType("date");

            builder
                  .Property(m => m.LastVisitDate)
                  .IsRequired()
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("(getdate())");

            builder
                  .Property(m => m.YandexLimit)
                  .HasDefaultValue(0);

            builder
                  .Property(m => m.GoogleLimit)
                  .HasDefaultValue(0);

            builder
                  .Property(m => m.FreeLimit)
                  .HasDefaultValue(0);

            builder
                .Property(m => m.AddDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())"); 

            builder
                .Property(m => m.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            #endregion members
        }
    }
}
