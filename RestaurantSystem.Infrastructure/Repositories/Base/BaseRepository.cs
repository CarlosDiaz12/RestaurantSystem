using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Entities.Base;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Data;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RestaurantSystem.Infrastructure.Repositories.Base
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "", int? take = null, int? skip = null)
        {
            IQueryable<TEntity> result = _dbSet;

            if (filter != null)
                result = result.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                result = result.Include(includeProperty);

            if (take.HasValue && skip.HasValue)
            {
                var pageIndex = skip.Value;
                var pageSize = take.Value;
                result = result.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }

            return await result.ToListAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter , string includeProperties = "")
        {
            IQueryable<TEntity> result = _dbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                result = result.Include(includeProperty);

            return await result.FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> GetById(int Id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task Delete(int Id)
        {
            var entity = await GetById(Id);
            if(entity != null)
                _dbSet.Remove(entity);
        }

        public async Task Insert(TEntity entity)
        {
            entity.DateCreated = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            entity.ModificationDate = DateTime.Now;

            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbContext.Attach(entity);

            _dbSet.Update(entity);
        }

        public async Task<bool> Exists(int Id)
        {
            return await _dbSet.AnyAsync(x => x.Id == Id);
        }

    }
}
