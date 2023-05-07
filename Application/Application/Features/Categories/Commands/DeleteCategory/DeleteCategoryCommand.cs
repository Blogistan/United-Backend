﻿using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<DeleteCategoryResponse>
    {
        public int Id { get; set; }
        public bool Permanent { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResponse>
        {
            private ICategoryRepository categoryRepository;
            private readonly CategoryBusinessRules categoryBusinessRules;
            public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, CategoryBusinessRules categoryBusinessRules)
            {
                this.categoryRepository = categoryRepository;
                this.categoryBusinessRules = categoryBusinessRules;
            }

            public async Task<DeleteCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await categoryBusinessRules.CategoryCheckById(request.Id);


                Category deletedCategory = await categoryRepository.DeleteAsync(category,request.Permanent);

                return new DeleteCategoryResponse
                {
                    Id = deletedCategory.Id,
                    CategoryName = deletedCategory.CategoryName
                };

            }
        }
    }
}
