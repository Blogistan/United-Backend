namespace Application.Features.Categories.Dtos
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public int? ParentCategoryIds { get; set; }
    }
}
