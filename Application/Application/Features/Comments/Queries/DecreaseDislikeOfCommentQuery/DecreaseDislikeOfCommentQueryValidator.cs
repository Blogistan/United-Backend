using FluentValidation;

namespace Application.Features.Comments.Queries.DecreaseDislikeOfCommentQuery
{
    public class DecreaseDislikeOfCommentQueryValidator:AbstractValidator<DecreaseDislikeOfCommentQuery>
    {
        public DecreaseDislikeOfCommentQueryValidator()
        {
            RuleFor(x=>x.CommentId).NotEmpty();
        }
    }
}
