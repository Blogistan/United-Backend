using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.EntityConfigurations
{
    public class VideoContentConfiguration : IEntityTypeConfiguration<VideoContent>
    {
        public void Configure(EntityTypeBuilder<VideoContent> builder)
        {
            builder.HasKey(x => new
            {
                x.ContentId,
                x.VideoId
            });

            builder.HasOne(x => x.Video)
                .WithMany(x=>x.VideoContents)
                .HasForeignKey(x=>x.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ContentItem)
                .WithMany(x => x.VideoContents)
                .HasForeignKey(x => x.ContentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
