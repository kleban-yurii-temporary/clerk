using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clerk.Core
{
    public static class ClerkContextModelBuilderExtension
    {
        private static void seedOptions(this ModelBuilder builder)
        {            

            builder.Entity<Option>()
              .HasData(
              new Option
              {
                  Key = "upload_path",
                  Value = "Uploads"
              },
              new Option
              {
                  Key = "territory_title",
                  Value = "Острозька ОТГ"
              });           
        }
        private static void seedRolesAndUsers(this ModelBuilder builder)
        {
            string ADMIN_ROLE_ID = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>()
              .HasData(
              new IdentityRole
              {
                  Name = "Admin",
                  NormalizedName = "ADMIN",
                  Id = ADMIN_ROLE_ID,
                  ConcurrencyStamp = ADMIN_ROLE_ID
              });

            string ADMIN_ID = Guid.NewGuid().ToString();

            var adminUser = new User
            {
                Id = ADMIN_ID,
                Email = "yurakleban@gmail.com",
                EmailConfirmed = true,
                UserName = "yurakleban@gmail.com",
                NormalizedUserName = "yurakleban@gmail.com".ToUpper()
            };           

            PasswordHasher<User> ph = new PasswordHasher<User>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "H76tT5rrf5$##w3");

            builder.Entity<User>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                });
        }

        public static void SeedFakeData(this ModelBuilder builder)
        {
            seedOptions(builder);
            seedRolesAndUsers(builder);
        }
    }    
}
