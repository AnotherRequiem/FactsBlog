using Requiem.Facts.Web.Infrastructure;

namespace Requiem.Facts.Web.Data
{
    public static class DataInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            const string username = "requiem@gmail.com";
            const string phone = "+380000000000";
            const string password = "12qa43Z05_";

            var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var isExists = context!.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator
                           && await databaseCreator.ExistsAsync();
            if (isExists)
            {
                return;
            }

            await context!.Database.MigrateAsync();

            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var roles = AppData.Roles.ToArray();
            IdentityResult identityResult;

            if (userManager == null || roleManager == null)
            {
                throw new MicroserviceArgumentNullException("UserManager or RoleManager not registered");
            }

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            if (await userManager.FindByEmailAsync(username) is not null)
            {
                return;
            }

            var user = new IdentityUser
            {
                UserName = username,
                Email = username,
                EmailConfirmed = true,
                PhoneNumber = phone,
                PhoneNumberConfirmed = true
            };

            identityResult = await userManager.CreateAsync(user, password);
            IdentityResultHandler(identityResult);

            identityResult = await userManager.AddToRolesAsync(user, roles);
            IdentityResultHandler(identityResult);

            await context.SaveChangesAsync();
        }

        private static void IdentityResultHandler(IdentityResult result)
        {
            if (result.Succeeded is false)
            {
                var message = string.Join(", ", result.Errors.Select(x => $"{x.Code}: {x.Description}"));
                throw new MicroserviceDatabaseException(message);
            }
        }
    }
}