using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Category : Entity<int>
    {
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }

        public virtual ICollection<Category>? SubCategories { get; set; }

        public Category()
        {

        }

        public Category(int id, string categoryName, ICollection<Category>? subCategories) : base(id)
        {
            this.Id = id;
            this.CategoryName = categoryName;
            this.SubCategories = subCategories;
        }
        public Category(int id, string categoryName) : base(id)
        {
            this.Id = id;
            this.CategoryName = categoryName;
        }

    }
}
