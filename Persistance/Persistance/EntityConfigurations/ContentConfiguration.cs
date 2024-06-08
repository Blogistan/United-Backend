using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            //builder.HasKey("Id");
            Content[] contents = {new(1,"The Mage Seekers","CONTENT_IMAGE_URL","Riot'un yeni oyunu pixel art'dan oluşan mage seekers 18.04.2023 tarihinde yayınlandı."),
                                    new(2,"OYUNDA ANINDA CRACKLANDİ","CONTENT_IMAGE_URL","Oyun 1 saat süre olmadan hackerlar tarafından kırıldı"),
                                    new(3,"Togg Yollarda","CONTENT_IMAGE_URL","Yerli üretim aracımız togg artık yollarda ön satışlar bitti."),
                                    new(4,"IOT nedir","IOT_IMAGE","Nesnelerin interneti, fiziksel nesnelerin birbirleriyle veya daha büyük sistemlerle bağlantılı olduğu iletişim ağıdır.İnternet üzerinden diğer cihazlara ve sistemlere bağlanmak ve veri alışverişi yapmak amacıyla sensörler, yazılımlar ve diğer teknolojilerle gömülüdür.")};

            builder.HasData(contents);
        }
    }
}
