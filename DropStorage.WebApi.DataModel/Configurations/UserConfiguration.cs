using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropStorage.WebApi.DataModel.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

            builder.Property(e => e.Login)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("login");

            builder.Property(e => e.Password)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("password");

            builder.Property(e => e.Address)
                    .HasMaxLength(150)
                    .HasColumnName("address");

            builder.Property(e => e.DirectoryHome)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("directory_home");

            builder.Property(e => e.LastName)
                    .HasMaxLength(150)
                    .HasColumnName("last_name");

            builder.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

            builder.Property(e => e.ProfilePhotoUrl)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("profile_photo_url");

            builder.Property(e => e.RolId).HasColumnName("rol_id");

            builder.HasOne(d => d.Rol)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK__Users__rol_id__30F848ED");

        }
    }
}
