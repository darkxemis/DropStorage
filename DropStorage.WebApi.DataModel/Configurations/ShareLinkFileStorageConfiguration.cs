using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropStorage.WebApi.DataModel.Configurations
{
    public class ShareLinkFileStorageConfiguration : IEntityTypeConfiguration<ShareLinkFileStorage>
    {
        public void Configure(EntityTypeBuilder<ShareLinkFileStorage> builder)
        {
            builder.ToTable("ShareLinkFileStorage");

            builder.HasIndex(e => e.IdShareLink, "ShareLinkFileStorage");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.IdFileStorage).HasColumnName("idFileStorage");

            builder.Property(e => e.IdShareLink).HasColumnName("idShareLink");

            builder.HasOne(d => d.IdFileStorageNavigation)
                .WithMany(p => p.ShareLinkFileStorages)
                .HasForeignKey(d => d.IdFileStorage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShareLink__idFil__2B0A656D");

            builder.HasOne(d => d.IdShareLinkNavigation)
                .WithMany(p => p.ShareLinkFileStorages)
                .HasForeignKey(d => d.IdShareLink)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShareLink__idSha__2A164134");
        }
    }
}
