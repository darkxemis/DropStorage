using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropStorage.WebApi.DataModel.Configurations
{
    public class ResetPasswordConfiguration : IEntityTypeConfiguration<ResetPasswordLink>
    {
        public void Configure(EntityTypeBuilder<ResetPasswordLink> builder)
        {
            builder.ToTable("ResetPasswordLink");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("createTime");

            builder.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("expirationDate");

            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.User)
                .WithMany(p => p.ResetPasswordLinks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ResetPass__user___03F0984C");
        }
    }
}
