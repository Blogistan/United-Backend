namespace Application.Features.Categories.Dtos
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategroyId { get; set; }
    }
}
