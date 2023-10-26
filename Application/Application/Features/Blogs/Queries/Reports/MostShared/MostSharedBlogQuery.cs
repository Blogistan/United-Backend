using Application.Features.Blogs.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Blogs.Queries.Reports.MostShared
{
    public class MostSharedBlogQuery:IRequest<MostSharedBlogQueryResponse>,ISecuredRequest
    {
        public DateFilter DateFilter { get; set; }
        public string[] Roles => new[] { "Admin", "Moderator", "Writer", "User" };
        public class MostSharedBlogQueryHandler : IRequestHandler<MostSharedBlogQuery, MostSharedBlogQueryResponse>
        {
            private readonly IMapper mapper;
            private readonly IBlogRepository blogRepository;
            public MostSharedBlogQueryHandler(IBlogRepository blogRepository, IMapper mapper)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
            }
            public async Task<MostSharedBlogQueryResponse> Handle(MostSharedBlogQuery request, CancellationToken cancellationToken)
            {
                DateTime today = DateTime.Now.Date;
                DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek); // Haftanın başlangıcı Pazartesi olsun varsayalım
                DateTime endOfWeek = startOfWeek.AddDays(7);

                dynamic blogs = "";
                switch (request.DateFilter)
                {
                    case DateFilter.Yestarday:
                        blogs = await blogRepository.GetListAsync(x => x.CreatedDate.Day - 1 == DateTime.Now.Day - 1, orderBy: order => order.OrderByDescending(x => x.ShareCount), include: inc => inc.Include(x => x.Writer).Include(x => x.Category));
                        break;
                    case DateFilter.Today:
                        blogs = await blogRepository.GetListAsync(x => x.CreatedDate.Day == DateTime.Now.Day, orderBy: order => order.OrderByDescending(x => x.ShareCount), include: inc => inc.Include(x => x.Writer).Include(x => x.Category));
                        break;
                    case DateFilter.ThisWeek:
                        blogs = await blogRepository.GetListAsync(x => x.CreatedDate >= startOfWeek && x.CreatedDate <= endOfWeek, orderBy: order => order.OrderByDescending(x => x.ShareCount), include: inc => inc.Include(x => x.Writer).Include(x => x.Category));
                        break;
                    case DateFilter.ThisMonth:
                        blogs = await blogRepository.GetListAsync(x => x.CreatedDate.Month == DateTime.Now.Month, orderBy: order => order.OrderByDescending(x => x.ShareCount), include: inc => inc.Include(x => x.Writer).Include(x => x.Category));
                        break;
                }

                var result = mapper.Map<MostSharedBlogQueryResponse>(blogs);

                return result;
            }
        }
    }
}
