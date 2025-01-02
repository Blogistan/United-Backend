using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Queries.GetListOperationClaim;
using Application.Features.OperationClaims.Queries.GetListOperationClaimDynamic;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OperationClaimController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };
            GetListOperationClaimQueryResponse response = await Mediator.Send(getListOperationClaimQuery);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreateOperationClaimResponse response = await Mediator.Send(createOperationClaimCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Create([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
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
        [HttpPost]
        public async Task<IActionResult> GetListDynamic([FromBody] GetListOperationClaimDynamicQuery getListOperationClaimDynamicQuery)
        {
            GetListOperationClaimDynamicQueryResponse response = await Mediator.Send(getListOperationClaimDynamicQuery);
            return Ok(response);
        }
    }
}
