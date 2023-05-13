using Application.Features.Videos.Commands.CreateRangeVideo;
using Application.Features.Videos.Commands.CreateVideo;
using Application.Features.Videos.Commands.DeleteVideo;
using Application.Features.Videos.Commands.UpdateVideo;
using Application.Features.Videos.Dtos;
using Application.Features.Videos.Queries.GetById;
using Application.Features.Videos.Queries.GetListVideo;
using Application.Features.Videos.Queries.GetListVideoDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VideoController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListVideoQuery getListVideoQuery = new()
            {
                PageRequest = pageRequest
            };
            VideoListDto videoListDto = await Mediator.Send(getListVideoQuery);

            return Ok(videoListDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetListDynamic([FromQuery] PageRequest pageRequest,[FromQuery] DynamicQuery dynamicQuery)
        {
            GetListVideoDynamicQuery  request = new()
            {
                PageRequest = pageRequest,
                DynamicQuery=dynamicQuery
            };
            VideoListDto videoListDto = await Mediator.Send(request);

            return Ok(videoListDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetVideoByIdQuery getVideoByIdQuery)
        {
            VideoViewDto videoViewDto = await Mediator.Send(getVideoByIdQuery);

            return Ok(videoViewDto);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CreateVideoCommand createVideoCommand)
        {
            CreateVideoResponse response = await Mediator.Send(createVideoCommand);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddRange([FromBody] CreateRangeVideoCommand createRangeVideoCommand)
        {
            CreateRangeVideoResponse response = await Mediator.Send(createRangeVideoCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVideoCommand updateVideoCommand)
        {
            UpdateVideoResponse response = await Mediator.Send(updateVideoCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteVideoCommand deleteVideoCommand)
        {
            DeleteVideoResponse response = await Mediator.Send(deleteVideoCommand);
            return Ok(response);
        }
    }
}
