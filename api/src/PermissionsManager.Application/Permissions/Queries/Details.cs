using MediatR;
using PermissionsManager.Application.Contracts;
using PermissionsManager.Application.Permissions.Dtos;
using PermissionsManager.Application.Permissions.Mappers;

namespace PermissionsManager.Application.Permissions.Queries
{
    public class Details
    {
        public class Query : IRequest<PermissionDto>
        {
            public int Id { get; set; }
        }

        public class Handler(IPermissionRepository permissionRepository) : IRequestHandler<Query, PermissionDto?>
        {
            public async Task<PermissionDto?> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await permissionRepository.Find(request.Id);

                if (result == null) return null;
                return PermissionPermissionDto.Map(result);
            }
        }

    }
}
