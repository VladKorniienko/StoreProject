using AutoMapper;
using StoreProject.BLL.Dtos.Category;
using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Interfaces;
using StoreProject.Common.Exceptions;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryPartialDto>> GetCategories()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryPartialDto>>(categories);
            return categoriesDto;
        }
        public async Task<CategoryDto> GetCategory(string id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<CategoryPartialDto> AddCategory(CategoryCreateDto newCategoryDto)
        {
            var category = await _unitOfWork.Categories.FindAsync(g => g.Name == newCategoryDto.Name);
            if (category.Any())
            {
                throw new ArgumentException($"Category with the same name ({newCategoryDto.Name}) already exists.");
            }
            //if the category doesn't exist, create new product in db
            var newCategory = _mapper.Map<Category>(newCategoryDto);
            await _unitOfWork.Categories.AddAsync(newCategory);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CategoryPartialDto>(newCategory);
        }
        public async Task<bool> UpdateCategory(CategoryCreateDto categoryToUpdate, string id)
        {
            var existingCategory = await _unitOfWork.Categories.GetByIdAsync(id);
            if (existingCategory == null)
            {
                throw new NotFoundException($"Category with ID {id} not found.");
            }
            //check if the category with the same name already exists in db
            var categoryWithNameDuplicate = await _unitOfWork.Categories.FindAsync(p => p.Name == categoryToUpdate.Name && p.Id != id);
            if (categoryWithNameDuplicate.Any())
            {
                throw new ArgumentException($"Category with the same name ({categoryToUpdate.Name}) already exists.");
            }
            //if the category exists, update it in db
            _mapper.Map(categoryToUpdate, existingCategory);
            await _unitOfWork.Categories.UpdateAsync(existingCategory);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteCategory(string id)
        {
            //check if the category exists in db
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException($"Category with ID {id} not found.");
            }
            //if the category exists, delete it from db
            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
