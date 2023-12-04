using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommandValidator : AbstractValidator<CreateUserOperationClaimCommand>
    {
        public CreateUserOperationClaimCommandValidator()
        {
            RuleFor(x => x.UserID).NotEmpty();
            RuleFor(x => x.OpreationClaimID).NotEmpty();
        }
    }
}
