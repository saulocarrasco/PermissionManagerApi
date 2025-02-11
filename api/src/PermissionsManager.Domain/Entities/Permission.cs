using System.ComponentModel.DataAnnotations;
using PermissionsManager.Domain.Contracts;

namespace PermissionsManager.Domain.Entities
{
    public class Permission : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string? EmployeeForename { get; set; }

        [Required]
        public string? EmployeeSurname { get; set; }

        [Required]
        public DateTime PermissionDate { get; set; }

        [Required]
        public int PermissionTypeId { get; set; }
        public virtual PermissionType? PermissionTypeType { get; set; }
    }
}
