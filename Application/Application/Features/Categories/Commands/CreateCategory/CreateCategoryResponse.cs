namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategroyId { get; set; }

    }
}
