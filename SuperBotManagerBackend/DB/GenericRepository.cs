using Microsoft.EntityFrameworkCore;
using SuperBotManagerBackend.DB.Repositories;
using System.Security.Principal;

namespace SuperBotManagerBackend.DB
{
    public interface IEntity
    {
        int Id { get; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFn = null);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(int id);
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly AppDBContext _dbContext;

        public GenericRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public async Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFn = null)
        {
            if(queryFn == null)
                queryFn = a => a;
            var userSet = _dbContext.Set<TEntity>();
            var query = queryFn(userSet.AsQueryable());

            return await query
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Update(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
