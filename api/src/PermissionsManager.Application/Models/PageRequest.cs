namespace PermissionsManager.Application.Models
{
    public abstract class PageRequest
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}
