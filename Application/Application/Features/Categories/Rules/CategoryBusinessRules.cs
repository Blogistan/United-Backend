﻿using Application.Features.Categories.Dtos;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Features.Categories.Rules
{
    public class CategoryBusinessRules : BaseBusinessRules
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryBusinessRules(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CategoryCannotBeDuplicatedWhenInserted(string categoryName)
        {
            Category category = await categoryRepository.GetAsync(x => x.CategoryName == categoryName);

            if (category != null)
            {
                throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "Category", Errors = new List<string> { "Category is exists." } } });
            }
        }
        public async Task CategoryCannotBeDuplicatedWhenInserted(List<CreateCategoryDto> createCategoryDtos)
        {
            foreach (var item in createCategoryDtos)
            {
                Category category = await categoryRepository.GetAsync(x => x.CategoryName == item.CategoryName);
                if (category != null)
                {
                    throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "Category", Errors = new List<string> { "Category is exists." } } });
                }
            }
        }
        public async Task CategoryCannotBeDuplicatedWhenUpdated(string categoryName)
        {
            Category category = await categoryRepository.GetAsync(x => x.CategoryName == categoryName);
            if (category != null)
            {
                throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "Category", Errors = new List<string> { "Category is exists." } } });
            }
        }
        public async Task CategoryCannotBeDuplicatedWhenUpdated(List<UpdateCategoryDto> updateCategoryDtos)
        {
            foreach (var item in updateCategoryDtos)
            {
                Category category = await categoryRepository.GetAsync(x => x.CategoryName == item.CategoryName);
                if (category != null)
                {
                    throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "Category", Errors = new List<string> { "Category is exists." } } });
                }
            }
        }

        public async Task<Category> CategoryCheckById(int id)
        {
            Category category = await categoryRepository.GetAsync(x => x.Id == id);
            if (category != null)
            {
                throw new ValidationException(new List<ValidationExceptionModel> { new ValidationExceptionModel { Property = "Category", Errors = new List<string> { "Category is not exists." } } });
            }

            return category;
        }
        public async Task<List<Category>> CategoryCheckById(List<DeleteRangeCategoryDto> deleteRangeDtos)
        {
            List<Category> categories = new();

            foreach (var item in deleteRangeDtos)
            {
                Category category = await categoryRepository.GetAsync(x => x.Id == item.Id);
                if (category == null) throw new NotFoundException($"Category Id:{item.Id} is not exists.");

                categories.Add(category);
            }
            return categories;

        }
    }
}
