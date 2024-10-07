using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            Category[] categories = { new(1, "Internet"), new(2,"Sosyal Medya"), new(3,"Yazılım"), new(4,"Oyun"), new(5,"Mobil Oyun"), new(6,"Yaşam"), new(7,"Sektörel"),
                                        new(8,"Otomobil"), new(9,"Yapay Zeka"), new(10,"Sinema ve Dizi"), new(11,"Bilim")};
            builder.HasData(categories);
        }
    }
}
