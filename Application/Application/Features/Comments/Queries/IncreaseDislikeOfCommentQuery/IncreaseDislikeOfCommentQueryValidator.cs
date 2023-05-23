using FluentValidation;

namespace Application.Features.Comments.Queries.IncreaseDislikeOfCommentQuery
{
    public class IncreaseDislikeOfCommentQueryValidator:AbstractValidator<IncreaseDislikeOfCommentQuery>
    {
        public IncreaseDislikeOfCommentQueryValidator()
        {
            RuleFor(x=>x.CommentId).NotEmpty();
        }
    }
}
