using Application.Features.Blogs.Queries.GetListBlog;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Blogs.Queries.BlogExtendedList
{
    public class BlogExtendedListQuery: IRequest<BlogExtendedListQueryResponse>
    {
        public PageRequest PageRequest { get; set; }
        public DynamicQuery DynamicQuery { get; set; }

        public class BlogExtendedListQueryHandler:IRequestHandler<BlogExtendedListQuery, BlogExtendedListQueryResponse>
        {
            private readonly IBlogRepository blogRepository;
            private readonly IMapper mapper;
            public BlogExtendedListQueryHandler(IBlogRepository blogRepository, IMapper mapper)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
            }

            public async Task<BlogExtendedListQueryResponse> Handle(BlogExtendedListQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Blog> paginate = await blogRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize, include: x => x.Include(x => x.Category).Include(x => x.Content).Include(x => x.Writer).ThenInclude(x=>x.User));

                BlogExtendedListQueryResponse response = mapper.Map<BlogExtendedListQueryResponse>(paginate);

                return response;
            }
        }
    }
}
