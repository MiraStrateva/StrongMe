namespace StrongMe.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using StrongMe.Common;
    using StrongMe.Data.Models;

    public class IdentitySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Any())
            {
                return;
            }

            var adminUser = new ApplicationUser
            {
                UserName = "MiraStrateva@abv.bg",
                Email = "MiraStrateva@abv.bg",
                SecurityStamp = "RandomSecurityStamp",
            };

            await userManager.CreateAsync(adminUser, "Strateva");

            await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdministratorRoleName);
        }
    }
}
