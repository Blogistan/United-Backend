using Application.Features.Videos.Dtos;
using Core.Application.Responses;

namespace Application.Features.Videos.Commands.UpdateRangeVideo
{
    public class UpdateRangeVideoResponse:IResponse
    {
        public List<VideoViewDto> Items { get; set; }

        public UpdateRangeVideoResponse(List<VideoViewDto> items)
        {
            Items = items;
        }
    }
}
