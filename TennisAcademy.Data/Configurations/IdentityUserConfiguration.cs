using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisAcademyApp.Data.Configurations
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> config)
        {
            config
                .HasData(SeededUser());
        }
        private IdentityUser SeededUser()
        {
            var user = new IdentityUser
            {
                Id = "seed-user-id-123",
                UserName = "coachadmin",
                NormalizedUserName = "COACHADMIN",
                Email = "coachadmin@example.com",
                NormalizedEmail = "COACHADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(
                    new IdentityUser { UserName = "coachadmin@example.com" },
                    "Pass123!")
            };
            return user;
        }
    }
}
