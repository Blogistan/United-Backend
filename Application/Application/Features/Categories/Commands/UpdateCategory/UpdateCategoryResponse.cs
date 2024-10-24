using Core.Application.Responses;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryResponse :IResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategroyId { get; set; }

        public UpdateCategoryResponse()
        {
            
        }

        public UpdateCategoryResponse(int ıd, string categoryName, int parentCategroyId)
        {
            Id = ıd;
            CategoryName = categoryName;
            ParentCategroyId = parentCategroyId;
        }
    }
}
