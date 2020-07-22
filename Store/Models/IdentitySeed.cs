using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Store.Models
{
    public static class IdentitySeed
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<User> userManager = app.ApplicationServices.GetRequiredService<UserManager<User>>();
            User user = await userManager.FindByIdAsync(adminUser);

            if (user == null)
            {
                user = new User { UserName = "Admin" };
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
