namespace Application.Features.Categories.Dtos
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
        public int? ParentCategoryIds { get; set; }
    }
}
