using Application.Features.Videos.Dtos;
using Core.Application.Responses;

namespace Application.Features.Videos.Commands.CreateRangeVideo
{
    public class CreateRangeVideoResponse:IResponse
    {
        public List<VideoViewDto> Items { get; set; }


        public CreateRangeVideoResponse()
        {
            
        }
        public CreateRangeVideoResponse(List<VideoViewDto> items)
        {
            Items = items;
        }
    }
}
