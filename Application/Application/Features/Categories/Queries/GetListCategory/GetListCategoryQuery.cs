using Application.Features.Categories.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Queries.GetListCategory
{
    public class GetListCategoryQuery : IRequest<CategoryListDto>
    {
        public PageRequest PageRequest { get; set; }


        public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, CategoryListDto>
        {
            private readonly ICategoryRepository categoryRepository;
            private readonly IMapper mapper;
            public GetListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
            {
                this.categoryRepository = categoryRepository;
                this.mapper = mapper;
            }

            public async Task<CategoryListDto> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Category> paginate = await categoryRepository.GetListAsync(withDeleted: false, include: x => x.Include(x => x.SubCategories)
                , size: request.PageRequest.PageSize, index: request.PageRequest.Page);


                CategoryListDto categoryListDtos = mapper.Map<CategoryListDto>(paginate);

                return categoryListDtos;

            }
        }
    }
}
