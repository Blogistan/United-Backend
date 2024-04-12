using Application.Features.SiteUsers.Commands.CreateSiteUser;
using Application.Features.SiteUsers.Commands.DeleteSiteUser;
using Application.Features.SiteUsers.Commands.UpdateSiteUser;
using Application.Features.SiteUsers.Queries.GetById;
using Application.Features.SiteUsers.Queries.GetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SiteUserController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSiteUserCommand createSiteUserCommand)
        {
            CreateSiteUserResponse response = await Mediator.Send(createSiteUserCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSiteUserCommand updateSiteUserCommand)
        {
            UpdateSiteUserCommandResponse response = await Mediator.Send(updateSiteUserCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteSiteUserCommand deleteSiteUserCommand)
        {
            DeleteSiteUserCommandResponse response = await Mediator.Send(deleteSiteUserCommand);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSiteUserQuery getListSiteUserQuery = new GetListSiteUserQuery(pageRequest);
            GetListSiteUserQueryResponse response = await Mediator.Send(getListSiteUserQuery);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdSiteUserQuery getByIdSiteUserQuery)
        {
            GetByIdSiteUserQueryResponse response = await Mediator.Send(getByIdSiteUserQuery);
            return Ok(response);
        }

    }
}
