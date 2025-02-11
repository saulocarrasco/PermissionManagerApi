using Microsoft.EntityFrameworkCore.Storage;
using PermissionsManager.Application.Models;
using PermissionsManager.Domain.Contracts;

namespace PermissionsManager.Application.Contracts
{
    public interface IRepositoryBase<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> Find(int id);
        void Update(TEntity entity);
        Task Insert(TEntity entity);
        Task<(IEnumerable<TEntity> Entities, int Count)> GetAllPaginated(PageRequest request, int defaultOffset = 0, int defaultLimit = 20);

        Task<IDbContextTransaction> BeginTransaction();
        Task<int> SaveChanges();
    }
}
