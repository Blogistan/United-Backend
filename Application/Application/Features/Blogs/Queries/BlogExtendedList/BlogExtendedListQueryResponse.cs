using Application.Features.Blogs.Dtos;
using Core.Application.Responses;

namespace Application.Features.Blogs.Queries.BlogExtendedList
{
    public record BlogExtendedListQueryResponse:IResponse
    {
        public List<BlogExtendedViewDto> Items { get; set; }

    }

}
