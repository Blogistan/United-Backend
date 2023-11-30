﻿using Application.Features.Bans.Commands.CreateBan;
using Application.Features.Bans.Commands.DeleteBan;
using Application.Features.Bans.Commands.UpdateBan;
using Application.Features.Bans.Queries.GetListBans;
using Application.Features.Bans.Queries.GetListBansDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BanController:BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListBansQuery getListBansQuery = new() { PageRequest = pageRequest };
            GetListBansQueryResponse response = await Mediator.Send(getListBansQuery);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetListDynamicReport([FromQuery] PageRequest pageRequest, [FromQuery] DynamicQuery dynamicQuery)
        {
            GetListBansDynamicQuery getListBansDynamicQuery = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery };
            GetListBansDynamicQueryResponse response = await Mediator.Send(getListBansDynamicQuery);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] CreateBanCommand createBanCommand)
        {
            CreateBanCommandResponse response = await Mediator.Send(createBanCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReport([FromBody] UpdateBanCommand updateBanCommand)
        {
            UpdateBanCommandResponse response = await Mediator.Send(updateBanCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReport([FromBody] DeleteBanCommand deleteBanCommand)
        {
            DeleteBanCommandResponse response = await Mediator.Send(deleteBanCommand);
            return Ok(response);
        }
    }
}
