using FluentValidation;

namespace Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentsCommandValidator:AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentsCommandValidator()
        {
            RuleFor(x => x.CommentContent).NotNull();
        }
    }
}
