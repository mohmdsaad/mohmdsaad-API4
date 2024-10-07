using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Ahmed Khaled",
                    Email = "ahmed@gmail.com",
                    UserName = "ahmedkhaled",
                    Address = new Address
                    {
                        FirstName = "Ahmed",
                        LastName = "Khaled",
                        City = "El Shrouk",
                        State = "Cairo",
                        Street = "3",
                        PostalCode = "12345"
                    }
                };
                await userManager.CreateAsync(user,"Password123!");
            }
        }
    }
}
