using Microsoft.EntityFrameworkCore;
using SuperBotManagerBackend.DB.Repositories;
using System.Security.Principal;

namespace SuperBotManagerBackend.DB
{
    public interface IEntity<TID>
    {
        TID Id { get; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
    public interface IGenericRepository<TEntity, TID> where TEntity : class, IEntity<TID>
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(TID id, Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFn = null);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TID id);
    }

    public class GenericRepository<TEntity, TID> : IGenericRepository<TEntity, TID>
        where TEntity : class, IEntity<TID>
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

        public async Task Delete(TID id)
        {
            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public async Task<TEntity> GetById(TID id, Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFn = null)
        {
            var idIdCorretType = id is int || id is Guid;
            if(!idIdCorretType)
                throw new Exception("Id type is not supported");
           
            if(queryFn == null)
            queryFn = a => a;
            var userSet = _dbContext.Set<TEntity>();
            var query = queryFn(userSet.AsQueryable());

            if(id is int)
                return await query
                            .AsNoTracking()
                            .FirstOrDefaultAsync(e => (int)(object)e.Id == (int)(object)id);
            else
                return await query
                            .AsNoTracking()
                            .FirstOrDefaultAsync(e => (Guid)(object)e.Id == (Guid)(object)id);
  
        }

        public async Task Update(TEntity entity)
        {
            _dbContext.Update(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
