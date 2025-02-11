using FluentValidation;
using MediatR;
using PermissionsManager.Application.Contracts;
using PermissionsManager.Application.Permissions.Dtos;
using PermissionsManager.Application.Permissions.Mappers;

namespace PermissionsManager.Application.Permissions.Commands
{
    public class Create
    {
        public class Command : PermissionDto, IRequest<PermissionDto>
        {
        }

        public class Handler(IPermissionRepository permissionRepository, IElasticSearchService elasticSearchService) : IRequestHandler<Command, PermissionDto>
        {
            public async Task<PermissionDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var permission = PermissionPermissionDto.Map(request);

                using (var transaction = await permissionRepository.BeginTransaction())
                {
                    await permissionRepository.Insert(permission);
                    await permissionRepository.SaveChanges();

                    var indexed = await elasticSearchService.Index(permission);

                    if (indexed) await transaction.CommitAsync();
                    else throw new Exception("There was an error indexing the record");
                }

                return PermissionPermissionDto.Map(permission);
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(p => p.Id)
                    .Empty();

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
