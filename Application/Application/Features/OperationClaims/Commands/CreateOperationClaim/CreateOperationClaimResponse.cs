using Core.Application.Responses;

namespace Application.Features.OperationClaims.Commands.CreateOperationClaim
{
    public class CreateOperationClaimResponse:IResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public CreateOperationClaimResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
