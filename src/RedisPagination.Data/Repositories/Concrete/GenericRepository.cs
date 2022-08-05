using Microsoft.EntityFrameworkCore;
using RedisPagination.Core;
using System.Linq.Expressions;

namespace RedisPagination.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, IEntity
    {
        private readonly AppEfDbContext _dbContext;
        private DbSet<TEntity> entities;

        public GenericRepository(AppEfDbContext dbContext)
        {
            _dbContext = dbContext;
            entities = _dbContext.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression) => await entities.Where(expression).ToListAsync();
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await entities.AsNoTracking().ToListAsync();
        public async Task<TEntity> GetByIdAsync(int entityId) => await entities.FindAsync(entityId);
        public async Task InsertAsync(TEntity entity) => await entities.AddAsync(entity);
        public void Remove(TEntity entity) => entities.GetType().GetProperty("IsDeleted").SetValue(entity, true);
        public void Update(TEntity entity) => entities.Update(entity);

    }
}
