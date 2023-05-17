using FluentValidation;

namespace Application.Features.Comments.Queries.IncreaseLikeOfCommentQuery
{
    public class IncreaseLikeOfCommentQueryValidator:AbstractValidator<IncreaseLikeOfCommentQuery>
    {
        public IncreaseLikeOfCommentQueryValidator()
        {
            RuleFor(x=>x.CommentId).NotEmpty();
        }
    }
}
