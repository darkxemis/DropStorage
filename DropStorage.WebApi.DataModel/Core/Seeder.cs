using DropStorage.WebApi.DataModel.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropStorage.WebApi.DataModel.Core
{
    public static class Seeder
    {
        public static void ApplySeeder<T>(this ModelBuilder modelBuilder, IEntityTypeSeeder<T> seeder) where T : class, IEntity
        {
            EntityTypeBuilder<T> builder = modelBuilder.Entity<T>();
            seeder.Seed(builder);
        }

    }
}
