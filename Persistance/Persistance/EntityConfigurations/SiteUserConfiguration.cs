using Core.Security.Hashing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class SiteUserConfiguration : IEntityTypeConfiguration<SiteUser>
    {
        public void Configure(EntityTypeBuilder<SiteUser> builder)
        {
            byte[] hash, salt;
            HashingHelper.CreatePasswordHash("Admin123", out hash, out salt);
            SiteUser[] siteUsers =
            {
                new(1, "Admin", "Admin", "esquetta@gmail.com", "", "",
                salt, hash, true, true, Core.Security.Enums.AuthenticatorType.None,
                 new List<Blog>(), new List<Bookmark>())
            };

            // Ensure it's mapped to the same table as User
            builder.ToTable("Users");

            // Seed data for SiteUser
            builder.HasData(siteUsers);
        }

    }
}
