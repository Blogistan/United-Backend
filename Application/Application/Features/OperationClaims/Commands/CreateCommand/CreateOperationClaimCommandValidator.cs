using FluentValidation;

namespace Application.Features.OperationClaims.Commands.CreateCommand
{
    public class CreateOperationClaimCommandValidator:AbstractValidator<CreateOperationClaimCommand>
    {
        public CreateOperationClaimCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty();
        }
    }
}
