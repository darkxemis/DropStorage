using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropStorage.WebApi.DataModel.Configurations
{
    public class FileStorageConfiguration : IEntityTypeConfiguration<FileStorage>
    {
        public void Configure(EntityTypeBuilder<FileStorage> builder)
        {
            builder.ToTable("FileStorage");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("createTime");

            builder.Property(e => e.Extension)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("extension");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");

            builder.Property(e => e.SizeMb).HasColumnName("sizeMB");

            builder.Property(e => e.Url)
                .HasMaxLength(2000)
                .HasColumnName("url");

            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.User)
                .WithMany(p => p.FileStorages)
                .HasForeignKey(d => d.UserId);
        }
    }
}
