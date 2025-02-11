using PermissionsManager.Application.Contracts;
using PermissionsManager.Domain.Entities;

namespace PermissionsManager.Persistence.Repositories
{
    public class PermissionRepository(DataContext dataContext) : RepositoryBase<Permission>(dataContext), IPermissionRepository
    {
    }
}
