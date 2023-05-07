namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategroyId { get; set; }
    }
}
