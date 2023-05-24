namespace Persistance.EntityConfigurations
{
    //public class VideoContentConfiguration : IEntityTypeConfiguration<VideoContent>
    //{
    //    public void Configure(EntityTypeBuilder<VideoContent> builder)
    //    {
    //        builder.HasKey(x => new
    //        {
    //            x.ContentId,
    //            x.VideoId
    //        });

    //        builder.HasOne(x => x.Video)
    //            .WithMany(x=>x.VideoContents)
    //            .HasForeignKey(x=>x.VideoId)
    //            .OnDelete(DeleteBehavior.Restrict);

    //        builder.HasOne(x => x.ContentItem)
    //            .WithMany(x => x.VideoContents)
    //            .HasForeignKey(x => x.ContentId)
    //            .OnDelete(DeleteBehavior.Restrict);
    //    }
    //}
}
