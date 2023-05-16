using FluentValidation;

namespace Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandValidator:AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(x=>x.Permanent).NotNull();
            RuleFor(x => x.Id).NotNull();
        }
                

    }
}
