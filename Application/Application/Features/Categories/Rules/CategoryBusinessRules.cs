using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Categories.Rules
{
    public class CategoryBusinessRules
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryBusinessRules(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CategoryCannotBeDuplicatedWhenInserted(string categoryName)
        {
            Category category = await categoryRepository.GetAsync(x => x.CategoryName == categoryName);
            if (category is not null)
                throw new BusinessException("Category is exist");
        }
        public async Task CategoryCannotBeDuplicatedWhenUpdated(string categoryName)
        {
            Category category = await categoryRepository.GetAsync(x => x.CategoryName == categoryName);
            if (category is not null)
                throw new BusinessException("Category is exist");
        }
        public async Task<Category> CategoryCheckById(int id)
        {
            Category category = await categoryRepository.GetAsync(x => x.Id == id);
            if (category == null) throw new BusinessException("Category is not exists.");

            return category;
        }
    }
}
