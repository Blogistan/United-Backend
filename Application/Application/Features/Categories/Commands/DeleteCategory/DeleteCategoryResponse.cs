using Core.Application.Responses;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryResponse :IResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public DeleteCategoryResponse()
        {
            
        }

        public DeleteCategoryResponse(int id, string categoryName)
        {
            Id = id;
            CategoryName = categoryName;
        }
    }
}
