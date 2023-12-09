using Application.Features.Bans.Queries.GetListBansDynamic;
using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand;
using Application.Features.UserOperationClaims.Queries.GetListUsersOperationClaims;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserOperationClaimController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };
            GetListUserOperationClaimQueryResponse response = await Mediator.Send(getListOperationClaimQuery);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetListDynamic([FromQuery] PageRequest pageRequest, [FromQuery] DynamicQuery dynamicQuery)
        {
            GetListBansDynamicQuery getListBansDynamicQuery = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery };
            GetListBansDynamicQueryResponse response = await Mediator.Send(getListBansDynamicQuery);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserOperationClaim([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
        {
            CreateUserOperationClaimCommandResponse response = await Mediator.Send(createUserOperationClaimCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUserOperationClaim([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
        {
            UpdateUserOperationClaimCommandResponse response = await Mediator.Send(updateUserOperationClaimCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOperationClaim([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
        {
            DeleteUserOperationClaimResponse response = await Mediator.Send(deleteUserOperationClaimCommand);
            return Ok(response);
        }
    }
}
