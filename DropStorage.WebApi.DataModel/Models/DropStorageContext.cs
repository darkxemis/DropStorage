using DropStorage.WebApi.DataModel.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DropStorage.WebApi.DataModel.Models
{
    public partial class DropStorageContext : DbContext
    {
        public string connectionString { set; get; }

        public DropStorageContext()
        {
        }

        public DropStorageContext(DbContextOptions<DropStorageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Configure(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private void Configure(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new LogStatusConfiguration());
            builder.ApplyConfiguration(new RolConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
