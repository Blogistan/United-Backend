using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class ReportTypeConfiguration : IEntityTypeConfiguration<ReportType>
    {
        public void Configure(EntityTypeBuilder<ReportType> builder)
        {

            ReportType[] reportTypes = new[] { new ReportType(1, "Negative Attitude", "Putting people down or being negative about the website or article"),
                                               new ReportType(2, "Verbal Abuse", "Including, but not limited to language that is unlawful, harmful, threatening, abusive, harassing, defamatory, vulgar, obscene, sexually explicit, or otherwise objectionable."),
                                            new ReportType(3,"Hate Speech","Racism, sexism, homophobia, etc."),
                                            new ReportType(4,"Harassment","Unwanted behavior, physical or verbal (or even suggested), that makes a reasonable person feel uncomfortable, humiliated, or mentally distressed."),
                                            new ReportType(5,"Spam","Stupid pointless annoying articles")

            };

            builder.HasData(reportTypes);
        }
    }
}
