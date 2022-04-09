using System.Linq;
using InvestControl.Domain.Entity;

namespace InvestControl.Domain.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        TEntity Select(int id);
        IQueryable<TEntity> Query();
        IQueryable<TEntity> QueryReadOnly();
    }
}