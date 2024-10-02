using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class SiteUserConfiguration : IEntityTypeConfiguration<SiteUser>
    {
        public void Configure(EntityTypeBuilder<SiteUser> builder)
        {

            SiteUser[] siteUsers =
            {
                new(1,1,"https://res.cloudinary.com/db4z2k45t/image/upload/v1723911498/file_cq8ff5.png","Test Bio",true,new List<Blog>(),new List<Bookmark>())
            };

           
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
                .WithOne(b => b.SiteUser)
                .HasForeignKey(b => b.SiteUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(su => su.Reports)
                .WithOne(r => r.SiteUser)
                .HasForeignKey(r => r.SiteUserId)
                .OnDelete(DeleteBehavior.Restrict);

           
         
            builder.HasData(siteUsers);
        }

    }
}
