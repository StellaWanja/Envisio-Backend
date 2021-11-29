using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Envisio.Models;
using Microsoft.AspNetCore.Identity;

namespace Envisio.Data
{
    public class Seeder
    {
        public async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager,  AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Users.Any())
            {

                List<string> roles = new List<string> { "Admin", "Regular"};

                foreach (var role in roles)
                {
                  await  roleManager.CreateAsync(new IdentityRole { Name = role });
                }


                List<AppUser> users = new List<AppUser>
                {
                    new AppUser
                    {
                        FirstName = "Luke",
                        LastName = "Walker",
                        HospitalName = "Sky",
                        Email = "luke@gmail.com",
                        UserName = "luke"
                    },
                    new AppUser
                    {
                        FirstName = "Rose",
                        LastName = "Lucy",
                        HospitalName = "Penda",
                        Email = "rose@gmail.com",
                        UserName = "rosy"
                    }
                };


                foreach (var user in users)
                {
                   await userManager.CreateAsync(user, "P@ssW0rd");
                    if (user == users[0])
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        //must be both regular and admin to handle dcelete
                        await userManager.AddToRoleAsync(user, "Admin");
                        await userManager.AddToRoleAsync(user, "Regular");
                    }

                }
            }
        }
    }
}
