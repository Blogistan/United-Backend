using Application.Features.Contents.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contents.Queries.GetById
{
    public class GetContentByIdQuery : IRequest<GetContentByIdQueryResponse>, ISecuredRequest
    {
        public int Id { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin", "Moderator", "Blogger", "User" };

        public class GetContentByIdQueryHandler : IRequestHandler<GetContentByIdQuery, GetContentByIdQueryResponse>
        {
            private readonly IContentRepository contentRepository;
            private readonly IMapper mapper;
            public GetContentByIdQueryHandler(IContentRepository contentRepository, IMapper mapper)
            {
                this.contentRepository = contentRepository;
                this.mapper = mapper;
            }
            public async Task<GetContentByIdQueryResponse> Handle(GetContentByIdQuery request, CancellationToken cancellationToken)
            {
                Content content = await contentRepository.GetAsync(x => x.Id == request.Id);

                GetContentByIdQueryResponse getContentByIdQueryResponse = new GetContentByIdQueryResponse();

                getContentByIdQueryResponse.Content = mapper.Map<ContentListViewDto>(content);

                return getContentByIdQueryResponse;
            }
        }
    }
}
