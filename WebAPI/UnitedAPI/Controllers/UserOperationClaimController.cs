﻿using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand;
using Application.Features.UserOperationClaims.Queries.GetListUsersOperationClaims;
using Application.Features.UserOperationClaims.Queries.GetListUsesrOperationClaimDynamic;
using Core.Application.Requests;
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
        [HttpPost]
        public async Task<IActionResult> GetListDynamic([FromBody] GetListUserOperationClaimDynamicQuery getListUserOperationClaimDynamicQuery)
        {
            GetListUserOperationClaimDynamicQueryResponse response = await Mediator.Send(getListUserOperationClaimDynamicQuery);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
        {
            CreateUserOperationClaimCommandResponse response = await Mediator.Send(createUserOperationClaimCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
        {
            UpdateUserOperationClaimCommandResponse response = await Mediator.Send(updateUserOperationClaimCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
        {
            DeleteUserOperationClaimResponse response = await Mediator.Send(deleteUserOperationClaimCommand);
            return Ok(response);
        }
    }
}
