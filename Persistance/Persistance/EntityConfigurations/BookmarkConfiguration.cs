using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.HasNoKey();
            builder.HasKey(x => new
            {
                x.SiteUserId,
                x.BlogId
            });
            builder.HasOne(bc => bc.SiteUser)
                .WithMany(b => b.Bookmarks)
                .HasForeignKey(bc => bc.SiteUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bc => bc.Blog)
            .WithMany(c => c.FavoritedUsers)
            .HasForeignKey(bc => bc.BlogId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
