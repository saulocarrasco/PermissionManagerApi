using MediatR;
using PermissionsManager.Application.Contracts;
using PermissionsManager.Application.Models;
using PermissionsManager.Domain.Entities;

namespace PermissionsManager.Application.Permissions.Queries
{
    public class List
    {
        public class Query : PageRequest, IRequest<PermissionsEnvelope>
        {
        }

        public class Handler(IPermissionRepository permissionRepository) : IRequestHandler<Query, PermissionsEnvelope>
        {
            public async Task<PermissionsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var (permissions, count) = await permissionRepository.GetAllPaginated(request);

                return new()
                {
                    Permissions = permissions,
                    Count = count
                };
            }
        }

        public class PermissionsEnvelope
        {
            //PermissionDTO
            public required IEnumerable<Permission> Permissions { get; set; }
            public int Count { get; set; }
        }
    }
}
