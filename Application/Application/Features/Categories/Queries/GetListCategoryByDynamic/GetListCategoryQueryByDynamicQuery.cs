using Application.Features.Categories.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Queries.GetListCategoryByDynamic
{
    public class GetListCategoryQueryByDynamicQuery:IRequest<CategoryListDto>
    {
        public DynamicQuery Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListCategoryQueryByDynamicQueryHandler:IRequestHandler<GetListCategoryQueryByDynamicQuery, CategoryListDto>
        {
            private readonly ICategoryRepository categoryRepository;
            private readonly IMapper mapper;
            public GetListCategoryQueryByDynamicQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                this.categoryRepository = categoryRepository;
                this.mapper = mapper;
            }

            public async Task<CategoryListDto> Handle(GetListCategoryQueryByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Category> paginate = await categoryRepository.
                    GetListByDynamicAsync(dynamic: request.Dynamic,
                    include: x => x.Include(x=>x.SubCategories),
                    withDeleted:false,
                    size:request.PageRequest.PageSize,
                    index:request.PageRequest.Page);

                CategoryListDto categoryListDtos = mapper.Map<CategoryListDto>(paginate);

                return categoryListDtos;



            }
        }
    } 
}

