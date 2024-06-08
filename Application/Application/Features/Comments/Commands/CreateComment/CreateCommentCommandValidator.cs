using FluentValidation;

namespace Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator:AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x=>x.CommentContent).NotNull();
            RuleFor(x=>x.BlogId).NotNull();
            
        }
    }
}
