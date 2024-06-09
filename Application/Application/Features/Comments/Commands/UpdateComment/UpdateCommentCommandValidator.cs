using FluentValidation;

namespace Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandValidator:AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.CommentContent).NotNull();
            RuleFor(x => x.Id).NotNull();
        }
    }
}
