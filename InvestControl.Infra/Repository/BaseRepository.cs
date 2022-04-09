using System.Linq;
using InvestControl.Domain.Entity;
using InvestControl.Domain.Repository;
using InvestControl.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InvestControl.Infra.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly InvestControlContext _context;

        protected BaseRepository(InvestControlContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var entity = Select(id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public TEntity Select(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> QueryReadOnly()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }
    }
}