namespace PermissionsManager.Application.Permissions.Dtos
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string? EmployeeForename { get; set; }
        public string? EmployeeSurname { get; set; }
        public DateTime PermissionDate { get; set; }
        public string? PermissionTypeDescription { get; set; }
        public int PermissionTypeId { get; set; }
    }
}
