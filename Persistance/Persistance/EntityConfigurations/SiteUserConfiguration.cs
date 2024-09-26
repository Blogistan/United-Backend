using Core.Security.Hashing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

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


            // Configure the relationships
            builder
                .HasMany(u => u.UserOperationClaims)
                .WithOne()
                .HasForeignKey(uoc => uoc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(su => su.Blogs)
                .WithOne(b => b.Writer)
                .HasForeignKey(b => b.WriterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(su => su.Bookmarks)
                .WithOne(bm => bm.SiteUser)
                .HasForeignKey(bm => bm.SiteUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(su => su.Bans)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(su => su.Reports)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure it's mapped to the same table as User
            

            // Seed data for SiteUser
            builder.HasData(siteUsers);
        }

    }
}
