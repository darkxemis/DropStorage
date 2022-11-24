using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropStorage.WebApi.DataModel.Configurations
{
    public class LogStatusConfiguration : IEntityTypeConfiguration<LogStatus>
    {
        public void Configure(EntityTypeBuilder<LogStatus> builder)
        {
            builder.ToTable("LogStatus");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.CreateTime)
                   .HasColumnType("datetime")
                   .HasColumnName("createTime");

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("description");

            builder.Property(e => e.Endpoint)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("endpoint");

            builder.Property(e => e.IsError).HasColumnName("isError");

            builder.Property(e => e.ParameterRecived).HasColumnName("parameterRecived");

            builder.Property(e => e.ParameterSended).HasColumnName("parameterSended");

            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(d => d.User)
                .WithMany(p => p.LogStatuses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__LogStatus__user___4AB81AF0");
        }
    }
}
