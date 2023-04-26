using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            Category[] categories = { new(1, "Internet"), new(2,"Sosyal Medya"), new(3,"Yazılım"), new(4,"Oyun"), new(5,"Mobil Oyun"), new(7,"Yaşam"), new(8,"Sektörel"),
                                        new(9,"Otomobil"), new(10,"Yapay Zeka"), new(11,"Sinema ve Dizi"), new(12,"Bilim")};
            builder.HasData(categories);
        }
    }
}
