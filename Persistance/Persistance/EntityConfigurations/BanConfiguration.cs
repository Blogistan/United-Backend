using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class BanConfiguration : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder.HasKey(x => new
            {
                x.ReportID,
                x.UserBanID
            });

            builder.HasOne(bc => bc.UserBan)
            .WithMany(b => b.Bans)
             .HasForeignKey(bc => bc.UserBanID)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bc => bc.Report)
            .WithMany(c => c.Bans)
            .HasForeignKey(bc => bc.ReportID)
            .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
