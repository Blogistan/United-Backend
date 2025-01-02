using Application.Features.Contents.Commands.CreateContent;
using Application.Features.Contents.Commands.DeleteContent;
using Application.Features.Contents.Commands.UpdateContent;
using Application.Features.Contents.Queries.GetListContent;
using Application.Features.Contents.Queries.GetListContentDynamic;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContentController:BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateContentCommand createContentCommand)
        {
            CreateContentCommandResponse response = await Mediator.Send(createContentCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateContentCommand updateContentCommand)
        {
            UpdateContentCommandResponse response = await Mediator.Send(updateContentCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteContentCommand deleteContentCommand)
        {
            DeleteContentCommandResponse response = await Mediator.Send(deleteContentCommand);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListContentQuery getListContentQuery = new() { PageRequest = pageRequest };
            GetListContentQueryResponse response = await Mediator.Send(getListContentQuery);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> GetListDynamic([FromBody] GetListContentDynamicQuery getListContentDynamicQuery)
        {
            GetListContentDynamicQueryResponse response = await Mediator.Send(getListContentDynamicQuery);
            return Ok(response);
        }

    }
}
