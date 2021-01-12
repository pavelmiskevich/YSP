using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YSP.Core.Models;

namespace YSP.Data.Configurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {

            #region Index
            builder
                .HasIndex(e => e.UserId)
                .HasName("Users_Feedbacks_FK");

            //builder
            //    .HasIndex(e => e.PageId)
            //    .HasName("Pages_Offers_FK");
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
            #endregion members
            #region reference
            //builder
            //    .HasOne(d => d.Page)
            //    .WithMany(p => p.Offers)
            //    .HasForeignKey(d => d.PageId);
            //.HasConstraintName("FK_OFFERS_REFERENCE_PAGES");

            builder
                .HasOne(m => m.User)
                .WithMany(p => p.Offers)
                .HasForeignKey(m => m.UserId);
            #endregion reference
        }
    }
}
