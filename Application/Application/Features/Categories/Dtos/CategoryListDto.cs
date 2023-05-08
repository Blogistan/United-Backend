namespace Application.Features.Categories.Dtos
{
    public class CategoryListDto
    {
        public List<CategoryViewDto> Items { get; set; }
    }

    public class CategoryViewDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<CategoryViewDto> SubCategories { get; set; }
    }
}
