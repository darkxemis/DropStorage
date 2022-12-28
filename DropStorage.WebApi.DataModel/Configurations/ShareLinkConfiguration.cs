using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropStorage.WebApi.DataModel.Configurations
{
    public class ShareLinkConfiguration : IEntityTypeConfiguration<ShareLink>
    {
        public void Configure(EntityTypeBuilder<ShareLink> builder)
        {

            builder.ToTable("ShareLink");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("expirationDate");

            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.User)
                .WithMany(p => p.ShareLinks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShareLink__user___17036CC0");
        }
    }
}
