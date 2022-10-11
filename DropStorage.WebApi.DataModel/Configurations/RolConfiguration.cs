using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropStorage.WebApi.DataModel.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {

            builder.ToTable("Rol");

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

            builder.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("description");

        }
    }
}
