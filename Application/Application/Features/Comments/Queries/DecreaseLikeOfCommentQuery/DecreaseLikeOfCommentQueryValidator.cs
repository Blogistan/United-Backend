using FluentValidation;

namespace Application.Features.Comments.Queries.DecreaseLikeOfCommentQuery
{
    public class DecreaseLikeOfCommentQueryValidator:AbstractValidator<DecreaseLikeOfCommentQuery>
    {
        public DecreaseLikeOfCommentQueryValidator()
        {
            RuleFor(x=>x.CommentId).NotEmpty();
        }
    }
}
