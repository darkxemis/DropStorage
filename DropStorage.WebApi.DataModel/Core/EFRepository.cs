using DropStorage.WebApi.DataModel.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DropStorage.WebApi.DataModel.Core
{
    public class EFRepository<TEFEntity> : IRepository<TEFEntity> where TEFEntity : class, IEntity
    {
        protected DbContext DbContext { get; private set; }

        protected DbSet<TEFEntity> DbSet { get; private set; }

        public EFRepository(DbContext context)
        {
            this.DbContext = context ?? throw new Exception($"EFRepository: {typeof(DbContext).Name} is null");
            this.DbSet = this.DbContext.Set<TEFEntity>();
        }

        public void Delete(TEFEntity entity)
        {
            EntityEntry dbEntityEntry = this.DbContext.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void DeleteRange(IEnumerable<TEFEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
        }

        public void Insert(TEFEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public void InsertRange(IEnumerable<TEFEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }

        public IQueryable<TEFEntity> Query()
        {
            return this.DbSet;
        }

        public void Update(TEFEntity entity)
        {
            EntityEntry dbEntityEntry = this.DbContext.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEFEntity> entities)
        {
            this.DbSet.UpdateRange(entities);
        }
    }
}
