using PermissionsManager.Domain.Entities;

namespace PermissionsManager.Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            await CreatePermissionTypes(context);
        }

        private static async Task CreatePermissionTypes(DataContext context)
        {
            if (!context.PermissionTypes.Any())
            {
                await context.PermissionTypes.AddRangeAsync(
                     new List<PermissionType>
                     {
                        new() { Description = "Personal"},
                        new() { Description = "Sickness"},
                        new() { Description = "Marriage" },
                        new() { Description = "Maternity/Paterninty"},
                        new() { Description = "Bereavement"},
                        new() { Description = "Other" },
                     }
                 );
                await context.SaveChangesAsync();
            }
        }
    }
}
