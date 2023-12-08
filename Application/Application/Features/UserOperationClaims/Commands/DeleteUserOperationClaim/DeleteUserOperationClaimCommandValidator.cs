using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimCommandValidator : AbstractValidator<DeleteUserOperationClaimCommand>
    {
        public DeleteUserOperationClaimCommandValidator()
        {
            RuleFor(x => x.OperationClaimId).NotEmpty();
            RuleFor(x => x.UserID).NotEmpty();
        }
    }
}
