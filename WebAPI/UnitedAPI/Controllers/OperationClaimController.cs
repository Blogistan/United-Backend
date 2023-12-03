using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Queries.GetListOperationClaim;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OperationClaimControlller : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };
            GetListOperationClaimQueryResponse response = await Mediator.Send(getListOperationClaimQuery);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddOperationClaim([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreateOperationClaimResponse response = await Mediator.Send(createOperationClaimCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOperationClaim([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            UpdateOperationClaimCommandResponse response = await Mediator.Send(updateOperationClaimCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOperationClaim([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            DeleteOperationClaimResponse response = await Mediator.Send(deleteOperationClaimCommand);
            return Ok(response);
        }
    }
}
