using Core.Application.Responses;

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim
{
    public record DeleteOperationClaimResponse :IResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DeleteOperationClaimResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
