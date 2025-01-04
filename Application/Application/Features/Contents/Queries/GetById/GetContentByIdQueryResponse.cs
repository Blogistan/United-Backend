using Application.Features.Contents.Dtos;
using Core.Application.Responses;

namespace Application.Features.Contents.Queries.GetById
{
    public class GetContentByIdQueryResponse:IResponse
    {
        public ContentListViewDto Content { get; set; }
    }
}
