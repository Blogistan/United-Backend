using FluentValidation;

namespace Application.Features.Bookmarks.Queries.RemoveFromBookmarks
{
    public class RemoveFromBookmarksQueryValidator:AbstractValidator<RemoveFromBookmarkQuery>
    {
        public RemoveFromBookmarksQueryValidator()
        {
            RuleFor(x=>x.UserId).NotEmpty();
            RuleFor(x=>x.BlogId).NotEmpty();
        }
    }
    
}
