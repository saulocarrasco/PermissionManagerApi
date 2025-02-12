using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PermissionsManager.Application.Contracts;
using PermissionsManager.Application.Models;
using PermissionsManager.Domain.Contracts;

namespace PermissionsManager.Persistence.Repositories
{
    public abstract class RepositoryBase<TEntity>(DataContext dataContext) : IRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        protected DataContext DataContext = dataContext;

        public async Task<TEntity?> Find(int id) => await DataContext.Set<TEntity>().FindAsync(id);
        public async Task<IEnumerable<TEntity>> GetAll() => await DataContext.Set<TEntity>().ToListAsync();
        public async Task Insert(TEntity entity) => await DataContext.Set<TEntity>().AddAsync(entity);
        public void Update(TEntity entity) => DataContext.Set<TEntity>().Update(entity);

        public Task<IDbContextTransaction> BeginTransaction() => DataContext.Database.BeginTransactionAsync();
        public Task<int> SaveChanges() => DataContext.SaveChangesAsync();

        public async Task<(IEnumerable<TEntity> Entities, int Count)> GetAllPaginated(PageRequest request, int defaultOffset = 0, int defaultLimit = 20)
        {
            var offset = request.Offset;
            var limit = request.Limit;

            if (offset < 0) offset = null;
            if (limit < 0) limit = null;

            var result = await DataContext.Set<TEntity>().OrderByDescending(x=>x.Id).Skip(offset ?? defaultOffset).Take(limit ?? defaultLimit).ToListAsync();

            return (result, DataContext.Set<TEntity>().Count());
        }
    }
}
