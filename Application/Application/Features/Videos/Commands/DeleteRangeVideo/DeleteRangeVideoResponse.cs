using Application.Features.Videos.Dtos;
using Core.Application.Responses;

namespace Application.Features.Videos.Commands.DeleteRangeVideo
{
    public class DeleteRangeVideoResponse:IResponse
    {
        public List<VideoViewDto> Items { get; set; }

        public DeleteRangeVideoResponse()
        {
            
        }
        public DeleteRangeVideoResponse(List<VideoViewDto> items)
        {
            Items = items;
        }
    }
}
