using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data
{
    public class DataSeeder
    {
        public RoleManager<IdentityRole> RoleManager { get; set; }

        public async Task SeedRoles()
        {
            var roles = new string[]
            {
                    "Адміністратор",
                    "Користувач"
            };
            foreach (var role in roles)
            {
                if (await RoleManager.FindByNameAsync(role) is null)
                    await RoleManager.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpperInvariant() });
            }
        }
    }
}
