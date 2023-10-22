using Application.Features.Blogs.Constants;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Blogs.Queries.Reports.MostReaded
{
    public class MostReadedBlogQuery : IRequest<MostReadedBlogQueryResponse>
    {
        public DateFilter DateFilter { get; set; }


        public class MostReadedBlogQueryHandler : IRequestHandler<MostReadedBlogQuery, MostReadedBlogQueryResponse>
        {
            private readonly IMapper mapper;
            private readonly IBlogRepository blogRepository;
            public MostReadedBlogQueryHandler(IBlogRepository blogRepository, IMapper mapper)
            {
                this.blogRepository = blogRepository;
                this.mapper = mapper;
            }

            public async Task<MostReadedBlogQueryResponse> Handle(MostReadedBlogQuery request, CancellationToken cancellationToken)
            {
                DateTime today = DateTime.Now.Date;
                DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek); // Haftanın başlangıcı Pazartesi olsun varsayalım
                DateTime endOfWeek = startOfWeek.AddDays(7);

                dynamic blogs = "";
                switch (request.DateFilter)
                {
                    case DateFilter.Yestarday:
                        blogs = await blogRepository.GetListAsync(x => x.CreatedDate.Day - 1 == DateTime.Now.Day - 1, orderBy: order => order.OrderByDescending(x => x.ReadCount), include: inc => inc.Include(x => x.Writer).Include(x => x.Category));
                        break;
                    case DateFilter.Today:
                        blogs = await blogRepository.GetListAsync(x => x.CreatedDate.Day == DateTime.Now.Day, orderBy: order => order.OrderByDescending(x => x.ReadCount), include: inc => inc.Include(x => x.Writer).Include(x => x.Category));
                        break;
                    case DateFilter.ThisWeek:
                        blogs = await blogRepository.GetListAsync(x => x.CreatedDate >= startOfWeek && x.CreatedDate <= endOfWeek, orderBy: order => order.OrderByDescending(x => x.ReadCount), include: inc => inc.Include(x => x.Writer).Include(x => x.Category));
                        break;
                    case DateFilter.ThisMonth:
                        blogs = await blogRepository.GetListAsync(x => x.CreatedDate.Month == DateTime.Now.Month, orderBy: order => order.OrderByDescending(x => x.ReadCount), include: inc => inc.Include(x => x.Writer).Include(x => x.Category));
                        break;
                }

                var result = mapper.Map<MostReadedBlogQueryResponse>(blogs);

                return result;


            }
        }
    }
}
