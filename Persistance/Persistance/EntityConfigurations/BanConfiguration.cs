using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class BanConfiguration : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder.HasOne(b => b.Report)
                .WithMany(r => r.Bans)
            .HasForeignKey(b => b.ReportId);

            builder.HasOne(b => b.SiteUser)
                .WithMany(u => u.Bans)
                .HasForeignKey(b => b.SiteUserId);
        }
    }
}
