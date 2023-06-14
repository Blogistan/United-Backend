using FluentValidation;

namespace Application.Features.Bookmarks.Queries.AddToBookmarks
{
   public class AddToBookmarksQueryValidator:AbstractValidator<AddToBookmarksQuery>
    {
        public AddToBookmarksQueryValidator()
        {
            RuleFor(x=>x.UserId).NotEmpty();
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
}
