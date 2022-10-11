using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DropStorage.WebApi.DataModel.Core.Abstractions
{
    public interface IEntityTypeSeeder<T> where T : class, IEntity
    {
        public abstract void Seed(EntityTypeBuilder<T> builder);
    }
}
