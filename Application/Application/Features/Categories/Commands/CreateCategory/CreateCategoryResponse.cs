using Core.Application.Responses;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryResponse:IResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategroyId { get; set; }

        public CreateCategoryResponse(int id, string categoryName, int? parentCategroyId)
        {
            Id = id;
            CategoryName = categoryName;
            ParentCategroyId = parentCategroyId;
        }

    }
}
