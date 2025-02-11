using PermissionsManager.Domain.Contracts;

namespace PermissionsManager.Application.Contracts
{
    public interface IElasticSearchService
    {
        Task<bool> Index<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task<bool> Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task Search<TEntity>(string term);
    }
}
