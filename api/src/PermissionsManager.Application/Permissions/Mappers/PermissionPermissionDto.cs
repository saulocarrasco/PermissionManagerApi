using PermissionsManager.Application.Permissions.Dtos;
using PermissionsManager.Domain.Entities;

namespace PermissionsManager.Application.Permissions.Mappers
{
    internal static class PermissionPermissionDto
    {
        public static PermissionDto Map(Permission permission)
        {
            return new PermissionDto
            {
                Id = permission.Id,
                EmployeeForename = permission.EmployeeForename,
                EmployeeSurname = permission.EmployeeSurname,
                PermissionDate = permission.PermissionDate,
                PermissionTypeId = permission.PermissionTypeId
            };
        }

        public static Permission Map(PermissionDto Dto, Permission? permission = null)
        {
            permission = permission ?? new Permission { PermissionDate = DateTime.UtcNow };

            permission.EmployeeForename = Dto.EmployeeForename;
            permission.EmployeeSurname = Dto.EmployeeSurname;
            permission.PermissionTypeId = Dto.PermissionTypeId;

            return permission;
        }
    }
}
