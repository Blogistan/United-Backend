using FluentValidation;

namespace Application.Features.Bookmarks.Queries.GetListBookmarks
{
    public class GetListBookmarkQueryValidator:AbstractValidator<GetListBookmarksQuery>
    {
        public GetListBookmarkQueryValidator()
        {
            RuleFor(x=>x.UserId).NotEmpty();
            
        }
    }
}
