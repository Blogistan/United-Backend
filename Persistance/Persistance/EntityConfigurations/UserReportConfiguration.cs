using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class UserReportConfiguration : IEntityTypeConfiguration<UserReport>
    {
        public void Configure(EntityTypeBuilder<UserReport> builder)
        {
            builder.HasKey(x => new { x.ReportID, x.SiteUserID });

            builder.HasOne(x => x.SiteUser)
                .WithMany(x => x.UserReports)
                .HasForeignKey(x => x.SiteUserID);

            builder.HasOne(x => x.Report)
                .WithMany(x => x.UserReports)
                .HasForeignKey(x => x.ReportID);
        }
    }
}
