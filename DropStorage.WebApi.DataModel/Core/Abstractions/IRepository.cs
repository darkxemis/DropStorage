using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropStorage.WebApi.DataModel.Core.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        void DeleteRange(IEnumerable<TEntity> entities);

        void UpdateRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Query();
    }
}
