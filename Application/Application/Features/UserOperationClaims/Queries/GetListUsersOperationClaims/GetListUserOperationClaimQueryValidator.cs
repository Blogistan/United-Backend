using FluentValidation;

namespace Application.Features.UserOperationClaims.Queries.GetListUsersOperationClaims
{
    public class GetListUserOperationClaimQueryValidator : AbstractValidator<GetListUserOperationClaimQuery>
    {
        public GetListUserOperationClaimQueryValidator()
        {
            RuleFor(x => x.PageRequest).NotEmpty();
        }
    }
}
