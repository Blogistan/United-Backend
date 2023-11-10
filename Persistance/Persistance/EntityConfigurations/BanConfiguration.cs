using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class BanConfiguration : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithMany(x=>x.Bans)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
