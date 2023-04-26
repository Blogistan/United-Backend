using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {

        public void Configure(EntityTypeBuilder<Video> builder)
        {
            Video[] videos = { new(1, "The Mageseekers", "https://youtube.com/watch/videolinki", "MageSeekers oynanış videosu"),
                new(2,"Togg Yollarda", "https://youtube.com/watch/videolinki","Togg sürüç derlemesi"),
                new(3,"Iot Nedir","https://youtube.com/watch/videolinki","Iot nedir,Günüümzde nerelerde kullanılmaktadır.")};

            builder.HasData(videos);
        }
    }
}
