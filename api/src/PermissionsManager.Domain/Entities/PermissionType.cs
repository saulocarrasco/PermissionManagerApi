using System.ComponentModel.DataAnnotations;
using PermissionsManager.Domain.Contracts;

namespace PermissionsManager.Domain.Entities
{
    public class PermissionType : IEntity
    {
        public int Id { get; set; }

        [Required]
        public required string Description { get; set; }
    }
}
