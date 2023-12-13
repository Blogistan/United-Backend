using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand
{
    public class UpdateUserOperationClaimCommandValidator:AbstractValidator<UpdateUserOperationClaimCommand>
    {
        public UpdateUserOperationClaimCommandValidator()
        {
            RuleFor(x=>x.UserId).NotNull();
            RuleFor(x=>x.OperationClaimId).NotNull();
        }
    }
}
