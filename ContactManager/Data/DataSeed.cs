using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            // Check if the password is null or empty
            if (string.IsNullOrEmpty(testUserPw))
            {
                throw new ArgumentException("Password must not be null or empty", nameof(testUserPw));
            }

            // Get services for user and role management
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Check if roles exist, if not, create them
            string[] roles = { "Admin", "Manager", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Check if test user exists, if not, create it
            var testUser = await userManager.FindByNameAsync("testuser@example.com");
            if (testUser == null)
            {
                testUser = new IdentityUser
                {
                    UserName = "testuser@example.com",
                    Email = "testuser@example.com"
                };
                var result = await userManager.CreateAsync(testUser, testUserPw);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(testUser, "Admin");
                }
            }

            var Manager = await userManager.FindByNameAsync("Manager@example.com");
            if (Manager == null)
            {
                Manager = new IdentityUser
                {
                    UserName = "Manager@example.com",
                    Email = "Manager@example.com"
                };
                var result = await userManager.CreateAsync(Manager, testUserPw);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(Manager, "Admin");
                }
            }
        }
    }
}
