using Microsoft.EntityFrameworkCore;
using PermissionsManager.Domain.Entities;

namespace PermissionsManager.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext() : base() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

    }
}
