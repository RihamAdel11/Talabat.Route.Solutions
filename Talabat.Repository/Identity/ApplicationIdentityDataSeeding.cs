using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
  public static class ApplicationIdentityDataSeeding
    {
        public static async Task SeedUsersAsync(UserManager <ApplicationUser >userManager) {

            if (!userManager.Users.Any())
            {
               var user = new ApplicationUser()
                {
                    DisplayName = "Riham Mohareb",

                    Email = "rihammohareb6@gmail.com",
                    PhoneNumber = "01210548629",
                    UserName = "Riham.Mohareb",
                };
               await userManager.CreateAsync(user, "P@ssw0rd");
            }
           
        }
    }
}
