using FluentValidation;
using MediatR;
using PermissionsManager.Application.Contracts;
using PermissionsManager.Application.Permissions.Dtos;
using PermissionsManager.Application.Permissions.Mappers;

namespace PermissionsManager.Application.Permissions.Commands
{
    public class Update
    {
        public class Command : PermissionDto, IRequest<int?>
        {
        }

        public class Handler(IPermissionRepository permissionRepository, IElasticSearchService elasticSearchService) : IRequestHandler<Command, int?>
        {
            public async Task<int?> Handle(Command request, CancellationToken cancellationToken)
            {
                var permission = await permissionRepository.Find(request.Id);
                if (permission == null) return null;

                PermissionPermissionDto.Map(request, permission);


                using (var transaction = await permissionRepository.BeginTransaction())
                {
                    permissionRepository.Update(permission);
                    await permissionRepository.SaveChanges();

                    var indexed = await elasticSearchService.Update(permission);

                    if (indexed) await transaction.CommitAsync();
                    else throw new Exception("There was an error indexing the record");
                }

                return permission.Id;
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(p => p.EmployeeForename)
                    .NotEmpty();

                RuleFor(p => p.EmployeeSurname)
                    .NotEmpty();

                RuleFor(p => p.PermissionTypeId)
                   .NotEmpty();
            }
        }
    }
}
