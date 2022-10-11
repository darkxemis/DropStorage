using DropStorage.WebApi.DataModel.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropStorage.WebApi.DataModel.Core
{
    public class EFUnitOfWork<TContext> : IUnitOfWork, IDisposable where TContext : DbContext
    {
        protected TContext DbContext { get; private set; }

        private Dictionary<Type, object> repositories = null;
        private bool disposed = false;

        public EFUnitOfWork(TContext context)
        {
            this.DbContext = context ?? throw new Exception($"EFUnitOfWork: {typeof(DbContext).Name} is null");
        }

        public EFRepository<TEFEntity> Repository<TEFEntity>()
            where TEFEntity : class, IEntity
        {
            object repository = this.GetRepository<TEFEntity>();
            if (repository == null)
            {
                Type type = typeof(EFRepository<TEFEntity>);
                repository = Activator.CreateInstance(type, this.DbContext);
                this.repositories.Add(type, repository);
            }

            return repository as EFRepository<TEFEntity>;
        }

        public async Task BeginTransactionAsync()
        {
            await this.DbContext.Database.BeginTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await this.DbContext.Database.RollbackTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await this.DbContext.Database.CommitTransactionAsync();
        }

        public bool Commit()
        {
            int entryCount = this.DbContext.SaveChanges();
            return entryCount != -1;
        }

        public async Task<bool> CommitAsync()
        {
            int entryCount = await this.DbContext.SaveChangesAsync();
            return entryCount != -1;
        }

        public async Task<bool> SaveChangesAsync()
        {
            int entryCount = await this.DbContext.SaveChangesAsync();
            return entryCount != -1;
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.DbContext.Dispose();
                this.disposed = true;
            }

            GC.SuppressFinalize(this);
        }

        private EFRepository<TEFEntity> GetRepository<TEFEntity>()
            where TEFEntity : class, IEntity
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<Type, object>();
            }

            Type type = typeof(EFRepository<TEFEntity>);
            object repository = null;
            if (this.repositories.ContainsKey(type))
            {
                repository = this.repositories[type];
                if (repository == null)
                {
                    this.repositories.Remove(type);
                }
            }

            return repository as EFRepository<TEFEntity>;
        }
    }
}
