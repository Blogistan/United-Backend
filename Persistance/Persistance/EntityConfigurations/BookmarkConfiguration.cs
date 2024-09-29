using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            // Define composite key
            builder.HasKey(x => new { x.SiteUserId, x.BlogId });

            // Configure relationships
            builder.HasOne(bc => bc.SiteUser)
                .WithMany(b => b.Bookmarks)
                .HasForeignKey(bc => bc.SiteUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bc => bc.Blog)
                .WithMany(c => c.FavoritedUsers)
                .HasForeignKey(bc => bc.BlogId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}