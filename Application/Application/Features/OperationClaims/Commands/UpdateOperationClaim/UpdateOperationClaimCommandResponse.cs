using Core.Application.Responses;

namespace Application.Features.OperationClaims.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommandResponse : IResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UpdateOperationClaimCommandResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }


    }
}
